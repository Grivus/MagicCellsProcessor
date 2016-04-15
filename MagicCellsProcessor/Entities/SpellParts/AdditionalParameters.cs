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

		public static AdditionalParametersStorage GetAdditionalParametersForMe( ISpellPart spellPart )
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
					{ "damageReduction", 2 },
					{ "hpRegeneration", 1 }
				};
			}
		}
	}

	public class AdditionalParametersStorage
	{
		public Dictionary<string, int> parameters;

		public AdditionalParametersStorage()
		{
			parameters = new Dictionary<string, int>();
			foreach ( var key in new List<string>( parameters.Keys ) )
				parameters[ key ] = 0;
		}

		public AdditionalParametersStorage( Dictionary<string, int> settings )
		{
			parameters = new Dictionary<string, int>( settings );
		}
	}
}
