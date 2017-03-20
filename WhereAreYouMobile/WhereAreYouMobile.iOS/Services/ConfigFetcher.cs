using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.iOS;
using WhereAreYouMobile.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ConfigFetcher))]

namespace WhereAreYouMobile.iOS
{
	/// <summary>
	/// Fetches settings from embedded resources in the Android project.
	/// </summary>
	public class ConfigFetcher : IConfigFetcher
	{
		#region IConfigFetcher implementation

		public async Task<string> GetAsync(ConfigurationsKeyEnum configElementName, bool readFromSensitiveConfig = false)
		{
			try
			{


				var fileName = (readFromSensitiveConfig) ? "config-sensitive.xml" : "config.xml";

				var type = this.GetType();
				var resource = type.Namespace + ".Config." + fileName;
				using (var stream = type.Assembly.GetManifestResourceStream(resource))
				{
					if (stream == null)
						throw new Exception("ConfigFetcher Error - Verifique el NameSpace");
					using (var reader = new StreamReader(stream))
					{
						var xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(await reader.ReadToEndAsync());

						if (xmlDocument.GetElementsByTagName(configElementName.ToString())[0] == null)
							throw new Exception("ConfigFetcher Error - No se encuentra la configuración solicitada");

						return xmlDocument.GetElementsByTagName(configElementName.ToString())[0].InnerText;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error al parsear el Json de configuraci¨®n al formato esperado");
			}
		}

		#endregion
	}


}





