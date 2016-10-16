using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.SpellParts
{
	public static class AdditionalParametersFactory
	{
		private static Dictionary<Type, IParametersSettings> typeSettingsMap = new Dictionary<Type, IParametersSettings>()
		{
			{ typeof(TestSpellPart), new AdditionalParametersTestSettings() }
		};

		public static AdditionalParametersStorage GetAdditionalParametersForMe( TestSpellPart spellPart )
		{
			return new AdditionalParametersStorage( typeSettingsMap[ spellPart.GetType() ].Parameters );
		}
	}

	public interface IParametersSettings
	{
		Dictionary<string, int> Parameters
		{
			get;
		}
	}

	public class AdditionalParametersTestSettings : IParametersSettings
	{
		public Dictionary<string, int> Parameters
		{
			get
			{
				return new Dictionary<string, int>()
				{
					{ "damageReduction", 0 },
					{ "hpRegeneration", 0 }
				};
			}
		}
	}

	public static class AdditionalParametersDefaultValues
	{
		public static Dictionary<string, int> keysAndDefaults = new Dictionary<string, int>()
		{
			{ "damageReduction", 0 },
			{ "hpRegeneration", 0 }
		};
	}

	public class AdditionalParametersStorage
	{
		public Dictionary<string, int> parameters;

		//public AdditionalParametersStorage()
		//{
		//	parameters = new Dictionary<string, int>();
		//	foreach ( var key in new List<string>( parameters.Keys ) )
		//		parameters[ key ] = 0;
		//}

		public AdditionalParametersStorage( Dictionary<string, int> settings )
		{
			parameters = new Dictionary<string, int>( settings );
		}

		public void ResetValues()
		{
			foreach ( var key in new List<string>( parameters.Keys ) )
				parameters[ key ] = 0;
		}

		public static AdditionalParametersStorage operator +( AdditionalParametersStorage s1, AdditionalParametersStorage s2 )
		{
			var result = new Dictionary<string, int>( s1.parameters );
			foreach ( var param in new List<string>( s1.parameters.Keys ) )
				result[ param ] = s1.parameters[ param ] + s2.parameters[ param ];

			return new AdditionalParametersStorage( result );
		}

	}
}
