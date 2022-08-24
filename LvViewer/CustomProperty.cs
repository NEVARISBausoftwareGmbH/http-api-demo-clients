using HttpApi_Wpf_Bommhardt;
using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lv_Viewer
{
    public class CustomProperty : NotifyPropertyChangedBase
    {
        KeyValuePair<string, CustomPropertyValue> _customProperty;
        public CustomProperty(KeyValuePair<string, CustomPropertyValue> customProperty, 
            LvItemBase? lvItemBase)
        {
            _customProperty = customProperty;
            NevarisLvItem = lvItemBase;           

            Init();
        }

        private void Init()
        {
            Name = _customProperty.Key;
            _wert = _customProperty.Value;
            OnPropertyChanged(nameof(Wert));
            ValueType = _customProperty.Value?.ValueType;
            if (ValueType != null)
            {
                switch (ValueType)
                {
                    case CustomPropertyType.Text:
                    case CustomPropertyType.DirectoryPath:
                        Wert = StringValue = _customProperty.Value?.StringValue;
                        break;
                    case CustomPropertyType.Date:
                    case CustomPropertyType.DateAndTime:
                        Wert = DateTimeValue = _customProperty.Value?.DateTimeValue;
                        break;
                    case CustomPropertyType.DateAndTimeWithTimeZone:
                        Wert = DateTimeWithTimeZoneValue = _customProperty.Value?.DateTimeWithTimeZoneValue;
                        break;
                    case CustomPropertyType.Time:
                        Wert = TimeSpanValue = _customProperty.Value?.TimeSpanValue;
                        break;
                    case CustomPropertyType.Duration:
                    case CustomPropertyType.Integer:
                        Wert = IntegerValue = _customProperty.Value?.IntegerValue;
                        break;
                    case CustomPropertyType.GeoCoordinate:
                        Wert = GeoCoordinateValue = _customProperty.Value?.GeoCoordinateValue;
                        break;
                    case CustomPropertyType.Boolean:
                        Wert = BooleanValue = _customProperty.Value?.BooleanValue;
                        break;
                    case CustomPropertyType.Decimal:
                        Wert = DecimalValue = _customProperty.Value?.DecimalValue;
                        break;
                    default:
                        break;
                }
            }
        }

        private LvItemBase? NevarisLvItem { get; set; }
        public CustomPropertyType? ValueType { get; private set; }
        private string? StringValue { get; set; }
        private decimal? DecimalValue { get; set; }
        private long? IntegerValue { get; set; }
        private bool? BooleanValue { get; set; }
        private DateTime? DateTimeValue { get; set; }
        private DateTimeOffset? DateTimeWithTimeZoneValue { get; set; }
        private TimeSpan? TimeSpanValue { get; set; }
        private GeoCoordinate? GeoCoordinateValue { get; set; }

        public bool IsModified { get; private set; }
        private object? _wert;
        public object? Wert
        {
            get { return _wert; }
            set
            {                
                _wert = value; 
                OnPropertyChanged(nameof(Wert));
                IsModified = true;   
                if (NevarisLvItem?.CustomPropertyValues != null)
                {
                    var customPropertyValue = new CustomPropertyValue()
                    {
                        DecimalValue = value != null ? decimal.Parse(value.ToString()) : null,
                        ValueType = CustomPropertyType.Decimal,
                    };

                    NevarisLvItem.CustomPropertyValues[Name] = customPropertyValue;
                }
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
    }
}
