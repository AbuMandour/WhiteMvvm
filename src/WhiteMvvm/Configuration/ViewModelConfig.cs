using System;
using Acr.UserDialogs;

namespace WhiteMvvm.Configuration
{
    public sealed class ViewModelConfig
    {
        private static readonly Lazy<ViewModelConfig> Lazy =new Lazy<ViewModelConfig>(() => new ViewModelConfig());
        public static ViewModelConfig Current => Lazy.Value;
        private ViewModelConfig()
        {
        }
        public bool UseBaseIndicator { get; set; } = true;
        public MaskType IndicatorMaskType { get; set; } = MaskType.Gradient;
    }
}
