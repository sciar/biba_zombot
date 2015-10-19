namespace BibaFramework.BibaMenu
{
    //TODO: make this auto generated just like the GameScene.cs
    public enum MenuStateTrigger
    {
        Next,
        Back,
        Scan,
        Yes,
        No,
        Reset,
        Settings,
        HowTo,
    }

    public enum MenuStateCondition
    {
        Inactive,

        //Player Settings
        PrivacyAgreementAccepted,
        TagEnabled,
        ShowHowTo,
        ShowHelpBubble,
    }
}