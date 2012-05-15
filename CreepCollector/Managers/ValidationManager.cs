using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace CreepCollector.Managers
{
    public static class ValidationManager
    {
        #region Validation Library

        private static Validators _validatorLib;
        /// <summary>
        /// Deserialized contents of validator.xml file.
        /// </summary>
        private static Validators validatorLib
        {
            get
            {
                if (_validatorLib == null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Validators));
                    string path = HttpContext.Current.ApplicationInstance.Server.MapPath(ConfigurationManager.AppSettings["ValidationXmlPath"]);
                    using (XmlReader reader = XmlReader.Create(path))
                    {
                        _validatorLib = (Validators)serializer.Deserialize(reader);
                    }
                }

                return _validatorLib;
            }
        }

        private static Dictionary<string, Dictionary<string, ValidatorsValidatorProperty>> _validators = null;
        /// <summary>
        /// Hashed set of validator data, based upon validator.xml file.
        /// Example usage:
        /// Validators["Contact"]["FirstName"].Required
        /// </summary>
        public static Dictionary<string, Dictionary<string, ValidatorsValidatorProperty>> Validators
        {
            get
            {
                if (_validators == null)
                {
                    _validators = new Dictionary<string, Dictionary<string, ValidatorsValidatorProperty>>();

                    // Convert the list of validators into a hash array for fast access. First, get each Validator entry.
                    foreach (var validatorItem in validatorLib.Items)
                    {
                        Dictionary<string, ValidatorsValidatorProperty> properties = new Dictionary<string, ValidatorsValidatorProperty>();

                        // Next, get each property entry for this validator and hash it by name.
                        foreach (var property in validatorItem.Property)
                        {
                            properties.Add(property.Name, property);
                        }

                        // Add the property hash to this validator.
                        _validators.Add(validatorItem.Type, properties);
                    }
                }

                return _validators;
            }
        }

        #endregion

        #region Validation Library Helpers

        /// <summary>
        /// Empties and refreshes the validator library. Reloads configuration from the xml file.
        /// </summary>
        public static void Refresh()
        {
            // Clear lists and set to null.
            _validators.Clear();
            _validators = null;

            _validatorLib = null;
        }

        #endregion
    }
}