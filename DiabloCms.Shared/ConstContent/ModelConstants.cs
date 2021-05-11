namespace DiabloCms.Shared.ConstContent
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinNameLength = 3;
            public const int NameLength = 50;
        }

        public class Identity
        {
            public const int MinEmailLength = 3;
            public const int EmailLength = 50;
            public const int MinPasswordLength = 6;
        }

        public class Address
        {
            public const int CountryLength = 255;
            public const int CityLength = 255;
            public const int StateLength = 255;
            public const int DescriptionLength = 1000;
            public const int PostalCodeLength = 10;
            public const int MinPhoneNumberLength = 5;
            public const int PhoneNumberLength = 20;
            public const string PhoneNumberRegularExpression = @"\+[0-9]*";
        }

        public class Product
        {
            public const int MinQuantity = 1;
            public const int Quantity = 100000;
            public const int UrlLength = 2048;
            public const int DescriptionLength = 1000;
            public const string MinPrice = "1";
            public const string Price = "100000";
        }
    }
}