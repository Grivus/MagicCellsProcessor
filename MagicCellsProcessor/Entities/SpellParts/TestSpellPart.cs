using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.SpellParts
{
	public class TestSpellPart : ISpellPart
	{
		public Cell CurrentCell
		{
			get
			{
				return currentCell;
			}
		}

		public Player PlayerOwner
		{
			get
			{
				return player;
			}
		}

		public HealthPoints HealthPoints
		{
			get
			{
				return healthPoints;
			}
		}

		public int Damage
		{
			get
			{
				return damage;
			}
		}

		public AdditionalParametersStorage AdditionalParameters
		{
			get
			{
				return additionalParameters;
			}
		}

		/// <summary>
		/// use only in GameProcessor!
		/// </summary>
		public int IndexToMove
		{
			get
			{
				return indexToMove;
			}

			set
			{
				indexToMove = value;
			}
		}

		/// <summary>
		/// use only in GameProcessor!
		/// </summary>
		public ISpellPart NearestEnemy
		{
			get
			{
				return nearestEnemy;
			}

			set
			{
				nearestEnemy = value;
			}
		}

		private Cell currentCell;

		/// <summary>
		/// player owns spell part
		/// </summary>
		private Player player;

		private HealthPoints healthPoints;

		private int damage;

		private AdditionalParametersStorage additionalParameters = new AdditionalParametersStorage();

		private List<Action> spellOrderList;

		private int indexToMove;

		private ISpellPart nearestEnemy;

		public TestSpellPart( Player owner, int hp, int damage )
		{
			player = owner;
			this.healthPoints = new HealthPoints( hp );
			this.damage = damage;

			spellOrderList = GetActionsOrderList();
		}

		protected virtual List<Action> GetActionsOrderList()
		{
			return new List<Action>()
			{
				RegenerateHP,
				Attack
			};
		}

		public void MoveTo( Cell destination )
		{
			destination.SetSpellPart( this );

			if ( currentCell != null )
				currentCell.RemoveSpellPart();

			currentCell = destination;
		}

		public void DoSelf()
		{
			foreach ( var action in spellOrderList )
			{
				action();
			}
		}

		public List<ISpellPart> GetNeighborsSpellParts()
		{
			var result = new List<ISpellPart>();
			var neighbors = currentCell.GetNeighborsCells();

			foreach ( var neighbor in neighbors )
				if ( neighbor.SpellPart != null )
					result.Add( neighbor.SpellPart );

			return result;
		}

		public List<ISpellPart> GetAlliedNeighborsSpellParts()
		{
			var result = new List<ISpellPart>();
			var neighbors = currentCell.GetNeighborsCells();

			foreach ( var neighbor in neighbors )
				if ( neighbor.SpellPart != null && neighbor.SpellPart.PlayerOwner.Id == this.player.Id )
					result.Add( neighbor.SpellPart );

			return result;
		}

		public List<ISpellPart> GetEnemyNeighborsSpellParts()
		{
			var result = new List<ISpellPart>();
			var neighbors = currentCell.GetNeighborsCells();

			foreach ( var neighbor in neighbors )
				if ( neighbor.SpellPart != null && neighbor.SpellPart.PlayerOwner.Id != this.player.Id )
					result.Add( neighbor.SpellPart );

			return result;
		}

		public void TakeDamage( int damage )
		{
			if ( additionalParameters.parameters.ContainsKey( "damageReduction" ) )
				damage = Utils.Clamp<int>( damage - additionalParameters.parameters[ "damageReduction" ], 0, damage );

			healthPoints.CurrentHp -= damage;
		}

		public void Die()
		{
			currentCell.RemoveSpellPart();
			player.SpellParts.Remove( this );
			//currentCell.Field.AllSpellParts.Remove( this );
		}

		private void RegenerateHP()
		{
			if ( additionalParameters.parameters.ContainsKey( "hpRegeneration" )  )
				healthPoints.CurrentHp += additionalParameters.parameters[ "hpRegeneration" ];
		}

		protected virtual void Attack()
		{
			var enemies = GetEnemyNeighborsSpellParts();

			if ( enemies.Count == 0 )
				return;

			var weakestEnemy = enemies[ 0 ];
			foreach ( var enemy in enemies )
			{
				if ( enemy.HealthPoints.CurrentHp < weakestEnemy.HealthPoints.CurrentHp )
					weakestEnemy = enemy;
			}

			weakestEnemy.TakeDamage( this.Damage );

		} 
		
	}
}
