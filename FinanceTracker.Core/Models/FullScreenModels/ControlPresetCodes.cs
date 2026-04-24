using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.FullScreenModels
{
    public enum InputPresetCode
    {
        Text = 1,
        Email = 2,
        Password = 3,
        Phone = 4,
        Number = 5,
        Float = 6,
        Link = 7,
        Time = 8,
        DateTime = 9,
        Between = 10,
    }

    public enum DropdownPresetCode
    {
        SingleSelect = 1,
        MultiSelect = 2,
    }

    public enum ButtonPresetCode
    {
        Button = 1,
        ButtonIcon = 2,
        Icon = 3,
    }

    public enum ContainerPresetCode
    {
        Section = 1,
        Group = 2,
        RadioGroup = 3,
    }
}
