using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ActAction
{
    private Dictionary<String, dynamic> _variables;
    private Action<Dictionary<String, dynamic>, double> _action;
    private Func<Dictionary<String, dynamic>, bool> _endCondition;
    private Action _postAction;

    public ActAction(
        Dictionary<String, dynamic> variables,
        Action<Dictionary<String, dynamic>, double> action,
        Func<Dictionary<String, dynamic>, bool> endCondition,
        Action postAction
    )
    {
        _variables = variables;
        _action = action;
        _endCondition = endCondition;
        _postAction = postAction;
    }

    public bool Act(double delta)
    {
        _action(_variables, delta);

        if (!_endCondition(_variables)) return false;

        _postAction();

        return true;
    }
}

public partial class StartMenu : Node
{

    [Export] public NodePath MainMenuPath;
    [Export] public NodePath NewGamePath;
    [Export] public NodePath LoadGamePath;
    [Export] public NodePath ExitPath;
    [Export] public NodePath LoadMenuPath;
    [Export] public NodePath Save1Path;
    [Export] public NodePath Save2Path;
    [Export] public NodePath Save3Path;
    [Export] public NodePath ReturnPath;

    private bool _buttonsDisabled = false;

    private bool _save1Exists = false;
    private bool _save2Exists = false;
    private bool _save3Exists = false;

    enum SubMenu { New, Load }
    private SubMenu _subMenu = SubMenu.New;


    private List<ActAction> _actions = new List<ActAction>();

    public override void _Ready()
    {
        _mainMenu = GetNode<BoxContainer>(MainMenuPath);
        _newGame = GetNode<Button>(NewGamePath);
        _loadGame = GetNode<Button>(LoadGamePath);
        _exit = GetNode<Button>(ExitPath);
        _loadMenu = GetNode<BoxContainer>(LoadMenuPath);
        _save1 = GetNode<Button>(Save1Path);
        _save2 = GetNode<Button>(Save2Path);
        _save3 = GetNode<Button>(Save3Path);
        _return = GetNode<Button>(ReturnPath);

        _save1Exists = FileAccess.FileExists("user://Saves/save1.json");
        _save2Exists = FileAccess.FileExists("user://Saves/save2.json");
        _save3Exists = FileAccess.FileExists("user://Saves/save3.json");

        _newGame.Pressed += IfEnabled(OnNewGamePressed);
        _loadGame.Pressed += IfEnabled(OnLoadGamePressed);
        _exit.Pressed += IfEnabled(OnExitPressed);

        _save1.Pressed += IfEnabled(OnSave1Pressed);
        _save2.Pressed += IfEnabled(OnSave2Pressed);
        _save3.Pressed += IfEnabled(OnSave3Pressed);
        _return.Pressed += IfEnabled(OnReturnPressed);
    }

    public override void _Process(double delta)
    {
        var action = _actions.First();
        if (action != null && action.Act(delta)) _actions.Remove(action);
    }

    private Action IfEnabled(Action operation)
    {
        return () =>
        {
            if (!_buttonsDisabled)
                operation();
        };
    }


    #region Main Menu

    private BoxContainer _mainMenu;
    private Button _newGame;
    private Button _loadGame;
    private Button _exit;

    private void OnNewGamePressed()
    {
        _subMenu = SubMenu.New;

        _save1.Disabled = false;
        _save2.Disabled = false;
        _save3.Disabled = false;

        _save1.Text = _save1Exists ? "Overwrite Save 1" : "Start Save 1";
        _save2.Text = _save2Exists ? "Overwrite Save 2" : "Start Save 2";
        _save3.Text = _save3Exists ? "Overwrite Save 3" : "Start Save 3";

        TransitionMenu(_mainMenu, _loadMenu, 1.0);
    }

    private void OnLoadGamePressed()
    {
        _subMenu = SubMenu.Load;

        if (_save1Exists) _save1.Disabled = true;
        if (_save2Exists) _save2.Disabled = true;
        if (_save3Exists) _save3.Disabled = true;

        _save1.Text = _save1Exists ? "Load Save 1" : "Empty";
        _save2.Text = _save1Exists ? "Load Save 2" : "Empty";
        _save3.Text = _save1Exists ? "Load Save 3" : "Empty";

        TransitionMenu(_mainMenu, _loadMenu, 1.0);
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }

    #endregion


    #region Load Menu

    private BoxContainer _loadMenu;
    private Button _save1;
    private Button _save2;
    private Button _save3;
    private Button _return;

    private void OnSave1Pressed()
    {

    }

    private void OnSave2Pressed()
    {
    }

    private void OnSave3Pressed()
    {
    }

    private void OnReturnPressed()
    {
        TransitionMenu(_loadMenu, _mainMenu, 1.0);
    }

    #endregion


    #region Menu Transition

    private void TransitionMenu(BoxContainer fromMenu, BoxContainer toMenu, double span)
    {
        _buttonsDisabled = true;

        double spanHalf = span / 2;

        var hideAction = new ActAction(
            variables: new Dictionary<string, dynamic>() { { "t", 0.0 } },
            action: (variables, delta) =>
            {
                double t = variables.GetValueOrDefault("t");
                double tNew = t + (delta / spanHalf);
                double tNewClamped = Mathf.Clamp(tNew, 0, 1);
                float a = (float)Mathf.Lerp(1, 0, tNewClamped);

                fromMenu.Modulate = new Color(1, 1, 1, a);

                variables["t"] = tNewClamped;
            },
            endCondition: (variables) =>
            {
                return variables.GetValueOrDefault("t") >= 1.0;
            },
            postAction: () =>
            {
                fromMenu.Visible = false;
                fromMenu.Modulate = new Color(1, 1, 1, 1);

                toMenu.Modulate = new Color(1, 1, 1, 0);
                toMenu.Visible = true;
            }
        );

        var showAction = new ActAction(
            variables: new Dictionary<string, dynamic>() { { "t", 0.0 } },
            action: (variables, delta) =>
            {
                double t = variables.GetValueOrDefault("t");
                double tNew = t + (delta / spanHalf);
                double tNewClamped = Mathf.Clamp(tNew, 0, 1);
                float a = (float)Mathf.Lerp(0, 1, tNewClamped);

                toMenu.Modulate = new Color(1, 1, 1, a);

                variables["t"] = tNewClamped;
            },
            endCondition: (variables) =>
            {
                return variables.GetValueOrDefault("t") >= 1.0;
            },
            postAction: () =>
            {
                _buttonsDisabled = false;
            }
        );

        _actions.Add(hideAction);
        _actions.Add(showAction);
    }

    #endregion


}