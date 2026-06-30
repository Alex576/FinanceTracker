using FinanceTracker.Core.Models.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Core.Models.FullScreenModels
{
    public enum ControlPresetCode
    {
        Separator = -1,

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
        SingleSelect = 101,
        MultiSelect = 102,
        Button = 201,
        ButtonIcon = 202,
        Icon = 203,
        Section = 301,
        Group = 302,
        RadioGroup = 303,
    }

    public static class ControlPresetCodeExtensions
    {
        public static string GetControlGroupName(this ControlPresetCode code)
        {
            var codeInt = (int)code;
            return codeInt switch
            {
                < 0 => "Server.Layout.Form.Preset.OthersGroup",
                > 0 and <= 100 => "Server.Layout.Form.Preset.InputGroup",
                > 100 and <= 200 => "Server.Layout.Form.Preset.DropdownGroup",
                > 200 and <= 300 => "Server.Layout.Form.Preset.ButtonGroup",
                > 300 and <= 400 => "Server.Layout.Form.Preset.ContainerGroup",
                _ => throw new NotImplementedException(),
            };
        }

        public static ControlType GetControlType(this ControlPresetCode code) => code switch
        {
            ControlPresetCode.Text => ControlType.Input,
            ControlPresetCode.Email => ControlType.Input,
            ControlPresetCode.Password => ControlType.Input,
            ControlPresetCode.Phone => ControlType.Input,
            ControlPresetCode.Number => ControlType.Input,
            ControlPresetCode.Float => ControlType.Input,
            ControlPresetCode.Link => ControlType.Text,
            ControlPresetCode.Time => ControlType.Input,
            ControlPresetCode.DateTime => ControlType.Input,
            ControlPresetCode.Between => ControlType.Input,
            ControlPresetCode.SingleSelect => ControlType.Combo,
            ControlPresetCode.MultiSelect => ControlType.Combo,
            ControlPresetCode.Button => ControlType.Button,
            ControlPresetCode.ButtonIcon => ControlType.Button,
            ControlPresetCode.Icon => ControlType.Button,
            ControlPresetCode.Section => ControlType.Group,
            ControlPresetCode.Group => ControlType.Group,
            ControlPresetCode.RadioGroup => ControlType.Group,
            ControlPresetCode.Separator => ControlType.Separator,
            _ => throw new NotImplementedException(),
        };
    }
}
