using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
using GDS.SharpDxResearch.GraphicObjects.Controls;

namespace GDS.SharpDxResearch.SharpDx.MainModule.Extensions
{
    [DictionaryKeyProperty("Key")]
    [ContentProperty("Value")]
    public class CustomResourceDictionaryValue
    {
        private Type key;
        [Key]
        public Type Key
        {
            get { return key; }
            set { key = value; }
        }

        public DxUserControl Value { get; set; }
    }
}
