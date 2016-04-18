using MagicCellsProcessor.Entities.Algorithms;
using MagicCellsProcessor.Entities.Algorithms.MoveStrategies;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.SpellParts
{
	public class TestSpellPart 
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
			set
			{
				additionalParameters = value;
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
		public NearestEnemyInfo NearestEnemyInfo
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

		public ISpellPartMoveStrategy MoveStrategy
		{
			get
			{
				return moveStrategy;
			}
		}

		private Cell currentCell;

		/// <summary>
		/// player owns spell part
		/// </summary>
		private Player player;

		private HealthPoints healthPoints;

		private int damage;

		private AdditionalParametersStorage additionalParameters;

		private List<Action> spellOrderList;

		private int indexToMove;

		private NearestEnemyInfo nearestEnemy;

		private ISpellPartMoveStrategy moveStrategy;

		public TestSpellPart( Player owner, int hp, int damage )
		{
			player = owner;
			this.healthPoints = new HealthPoints( hp );
			this.damage = damage;

			spellOrderList = GetActionsOrderList();

			additionalParameters = new AdditionalParametersStorage( AdditionalParametersDefaultValues.keysAndDefaults );

			moveStrategy = MoveStrategyFactory.GetMoveStrategyForMe( this );
		}

		protected virtual List<Action> GetActionsOrderList()
		{
			return new List<Action>()
			{
				RegenerateHP,
				Attack
			};
		}

		public void DoPremoveCalculations()
		{
			moveStrategy.DoCalculations( this );
		}

		public Cell ChooseCellToMove()
		{
			return moveStrategy.GetCellToMoveFor( this );
		}

		public void MoveTo( Cell destination )
		{
			if ( currentCell == destination )
			{
				Console.Write( "Fuck" );
			}

			destination.SetSpellPart( this );

			if ( currentCell != null )
				currentCell.RemoveSpellPart();

			currentCell = destination;

			if ( currentCell.SpellPart != this )
			{
				Console.Write( "Fuck" );
			}
		}

		public void DoSelf()
		{
			foreach ( var action in spellOrderList )
			{
				action();
			}
		}

		public List<TestSpellPart> GetNeighborsSpellParts()
		{
			var result = new List<TestSpellPart>();
			var neighbors = currentCell.GetNeighborsCells();

			foreach ( var neighbor in neighbors )
				if ( neighbor.SpellPart != null )
					result.Add( neighbor.SpellPart );

			return result;
		}

		public List<TestSpellPart> GetAlliedNeighborsSpellParts()
		{
			var result = new List<TestSpellPart>();
			var neighbors = currentCell.GetNeighborsCells();

			foreach ( var neighbor in neighbors )
				if ( neighbor.SpellPart != null && neighbor.SpellPart.PlayerOwner.Id == this.player.Id )
					result.Add( neighbor.SpellPart );

			return result;
		}

		public List<TestSpellPart> GetEnemyNeighborsSpellParts()
		{
			var result = new List<TestSpellPart>();
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
