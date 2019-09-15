using System;
using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.MenuUI;
using SharpDX;
using Color = System.Drawing.Color;

namespace DeveloperSharp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			GameEvent.OnGameLoad += new GameEvent.OnGameLoadDelegate(Program.GameEventOnOnGameLoad);
		}

		public static AIHeroClient Player
		{
			get
			{
				return ObjectManager.Player;
			}
		}

		private static void GameEventOnOnGameLoad()
		{
		    var menu = new Menu("xDreammsDeveloperSharp", "xDreamms DeveloperSharp",true);
		    menu.Attach();
            GameObject.OnCreate += Program.GameObjectOnOnCreate;
			GameObject.OnDelete += Program.GameObjectOnOnDelete;
			GameObject.OnMissileCreate += Program.GameObjectOnOnMissileCreate;
			AIBaseClient.OnBuffGain += Program.AiHeroClientOnOnBuffGain;
			AIBaseClient.OnDoCast += Program.AiHeroClientOnOnDoCast;
			GameObject.OnMissileCreate += Program.AiHeroClientOnOnMissileCreate;
			AIBaseClient.OnProcessSpellCast += Program.AiHeroClientOnOnProcessSpellCast;
			Drawing.OnDraw += Program.DrawingOnOnDraw;
			GameObject.OnCreate += Program.AiMinionClientOnOnCreate;
		}

		private static void GameObjectOnOnMissileCreate(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && Extensions.Distance(sender.Position, Program.Player.Position) < 1000f)
			{
				Program.OnOnMissileCreateObjectName = "Object Name: " + sender.Name;
				Program.OnOnMissileCreateObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnOnMissileCreateObjectType = "Object Type: " + sender.Type;
				Program.OnOnMissileCreateObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void GameObjectOnOnDelete(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && Extensions.Distance(sender.Position, Program.Player.Position) < 1000f)
			{
				Program.OnDeleteObjectName = "Object Name: " + sender.Name;
				Program.OnDeleteObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnDeleteObjectType = "Object Type: " + sender.Type;
				Program.OnDeleteObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void GameObjectOnOnCreate(GameObject sender, EventArgs args)
		{
			if (sender.Team == Program.Player.Team && Extensions.Distance(sender.Position, Program.Player.Position) < 1000f)
			{
				Program.OnCreateObjectName = "Object Name: " + sender.Name;
				Program.OnCreateObjectBoundingRadius = "Bounding Radius: " + sender.BoundingRadius;
				Program.OnCreateObjectType = "Object Type: " + sender.Type;
				Program.OnCreateObjectPosition = "Position: " + sender.Position;
			}
		}

		private static void AiHeroClientOnOnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
		{
			if (!sender.IsMe)
			{
				return;
			}
			Program.Slot = "SpellSlot: " + args.Slot;
			Program.CastTime = "CastTime: " + args.CastTime;
			Program.Time = "Tİme: " + args.Time;
			Program.TotalTime = "Total Time: " + args.TotalTime;
			Program.CastRadius = "Cast Radius: " + args.SData.CastRadius;
			Program.CastRange = "Cast Range:  " + args.SData.CastRange;
			Program.LineWidth = "Line Width : " + args.SData.LineWidth;
			Program.LineDragLength = "Line Drag Length: " + args.SData.LineDragLength;
			Program.MissileSpeed = "Missile Speed:  " + args.SData.MissileSpeed;
			Program.Name = "Name : " + args.SData.Name;
			Program.CastType = "Cast Type:  " + args.SData.CastType;
		}

		private static void AiHeroClientOnOnMissileCreate(GameObject sender, EventArgs args)
		{
			if (sender.IsAlly && Extensions.Distance(sender.Position, Program.Player.Position) < 1000f)
			{
				Program.MissileName = "Missile Name: " + sender.Name;
				Program.MissilePosition = "Missile Position: " + sender.Position;
				Program.MissileBoundingRadius = "Missile Bounding Radius: " + sender.BoundingRadius;
				Program.MissileNetworkID = "Missile Network ID: " + sender.NetworkId;
				Program.MissileType = "Missile Type: " + sender.Type;
			}
		}

		private static void AiHeroClientOnOnDoCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
		{
			if (sender.IsMe)
			{
				Program.OnDoCastSpellName = "Spell Name: " + args.SData.Name;
				Program.OnDoCastSpellSpeed = "Spell Speed: " + args.SData.MissileSpeed;
				Program.OnDoCastSpellCastRadius = "Cast Radius: " + args.SData.CastRadius;
				Program.OnDoCastCastRange = "Cast Range: " + args.SData.CastRange;
				Program.OnDoCastTime = "Cast Time: " + args.CastTime;
				Program.OnDoCastWidth = "Line Width: " + args.SData.LineWidth;
			}
		}

		private static void AiHeroClientOnOnBuffGain(AIBaseClient sender, AIBaseClientBuffGainEventArgs args)
		{
			if (args.Buff.Name.Contains("ASSETS"))
			{
				return;
			}
			if (sender.IsMe)
			{
				Program.MyBuffName = "My Buff Name: " + args.Buff.Name;
				Program.MyBuffCount = "My Buff Count: " + args.Buff.Count;
				Program.MyBuffType = "My Buff Type: " + args.Buff.Type;
				return;
			}
			Program.EnemyBuffName = "Enemy Buff Name: " + args.Buff.Name;
			Program.EnemyBuffCount = "Enemy Buff Count: " + args.Buff.Count;
			Program.EnemyBuffType = "Enemy Buff Type: " + args.Buff.Type;
		}

		private static void AiMinionClientOnOnCreate(GameObject sender, EventArgs args)
		{
			if (Extensions.Distance(sender.Position, Program.Player.Position) < 1000f && sender.Team == Program.Player.Team && !sender.Name.ToLower().Contains("turret"))
			{
				Program.MinionName = "Minion Name: " + sender.Name;
				Program.MinionType = "Minion Type: " + sender.Type;
				Program.MinionPosition = "Minion Position: " + sender.Position;
			}
		}

		private static void DrawingOnOnDraw(EventArgs args)
		{
			Drawing.DrawText(200f, 205f, Color.Red, "OnBuffGain");
			if (!string.IsNullOrWhiteSpace(Program.MyBuffName))
			{
				Drawing.DrawText(200f, 225f, Color.White, Program.MyBuffName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MyBuffCount))
			{
				Drawing.DrawText(200f, 240f, Color.White, Program.MyBuffCount);
			}
			if (!string.IsNullOrWhiteSpace(Program.MyBuffType))
			{
				Drawing.DrawText(200f, 255f, Color.White, Program.MyBuffType);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffName))
			{
				Drawing.DrawText(200f, 280f, Color.White, Program.EnemyBuffName);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffCount))
			{
				Drawing.DrawText(200f, 295f, Color.White, Program.EnemyBuffCount);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 310f, Color.White, Program.EnemyBuffType);
			}
			Drawing.DrawText(200f, 330f, Color.Red, "OnProcessSpellCast");
			if (!string.IsNullOrWhiteSpace(Program.Slot))
			{
				Drawing.DrawText(200f, 345f, Color.White, Program.Slot);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 360f, Color.White, Program.CastTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 375f, Color.White, Program.Time);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 390f, Color.White, Program.TotalTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 405f, Color.White, Program.CastRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 420f, Color.White, Program.CastRange);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 435f, Color.White, Program.LineWidth);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 450f, Color.White, Program.LineDragLength);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 465f, Color.White, Program.MissileSpeed);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 480f, Color.White, Program.Name);
			}
			if (!string.IsNullOrWhiteSpace(Program.EnemyBuffType))
			{
				Drawing.DrawText(200f, 495f, Color.White, Program.CastType);
			}
			Drawing.DrawText(200f, 515f, Color.Red, "OnMissileCreate");
			if (!string.IsNullOrWhiteSpace(Program.MissileName))
			{
				Drawing.DrawText(200f, 530f, Color.White, Program.MissileName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissilePosition))
			{
				Drawing.DrawText(200f, 545f, Color.White, Program.MissilePosition);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileBoundingRadius))
			{
				Drawing.DrawText(200f, 560f, Color.White, Program.MissileBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileNetworkID))
			{
				Drawing.DrawText(200f, 575f, Color.White, Program.MissileNetworkID);
			}
			if (!string.IsNullOrWhiteSpace(Program.MissileType))
			{
				Drawing.DrawText(200f, 590f, Color.White, Program.MissileType);
			}
			Drawing.DrawText(200f, 610f, Color.Red, "OnDoCast");
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellName))
			{
				Drawing.DrawText(200f, 625f, Color.White, Program.OnDoCastSpellName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellSpeed))
			{
				Drawing.DrawText(200f, 640f, Color.White, Program.OnDoCastSpellSpeed);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastSpellCastRadius))
			{
				Drawing.DrawText(200f, 655f, Color.White, Program.OnDoCastSpellCastRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastCastRange))
			{
				Drawing.DrawText(200f, 670f, Color.White, Program.OnDoCastCastRange);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastTime))
			{
				Drawing.DrawText(200f, 685f, Color.White, Program.OnDoCastTime);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDoCastWidth))
			{
				Drawing.DrawText(200f, 700f, Color.White, Program.OnDoCastWidth);
			}
			Drawing.DrawText(200f, 720f, Color.Red, "MinionCreate");
			if (!string.IsNullOrWhiteSpace(Program.MinionName))
			{
				Drawing.DrawText(200f, 735f, Color.White, Program.MinionName);
			}
			if (!string.IsNullOrWhiteSpace(Program.MinionType))
			{
				Drawing.DrawText(200f, 750f, Color.White, Program.MinionType);
			}
			if (!string.IsNullOrWhiteSpace(Program.MinionPosition))
			{
				Drawing.DrawText(200f, 765f, Color.White, Program.MinionPosition);
			}
			Drawing.DrawText(500f, 205f, Color.Red, "GameObjectOnCreate");
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectName))
			{
				Drawing.DrawText(500f, 225f, Color.White, Program.OnCreateObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectType))
			{
				Drawing.DrawText(500f, 240f, Color.White, Program.OnCreateObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectBoundingRadius))
			{
				Drawing.DrawText(500f, 255f, Color.White, Program.OnCreateObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnCreateObjectPosition))
			{
				Drawing.DrawText(500f, 270f, Color.White, Program.OnCreateObjectPosition);
			}
			Drawing.DrawText(500f, 290f, Color.Red, "GameObjectOnDelete");
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectName))
			{
				Drawing.DrawText(500f, 305f, Color.White, Program.OnDeleteObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectType))
			{
				Drawing.DrawText(500f, 320f, Color.White, Program.OnDeleteObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectBoundingRadius))
			{
				Drawing.DrawText(500f, 335f, Color.White, Program.OnDeleteObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnDeleteObjectPosition))
			{
				Drawing.DrawText(500f, 350f, Color.White, Program.OnDeleteObjectPosition);
			}
			Drawing.DrawText(500f, 370f, Color.Red, "GameObjectOnMissileCreate");
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectName))
			{
				Drawing.DrawText(500f, 385f, Color.White, Program.OnOnMissileCreateObjectName);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectType))
			{
				Drawing.DrawText(500f, 400f, Color.White, Program.OnOnMissileCreateObjectType);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectBoundingRadius))
			{
				Drawing.DrawText(500f, 415f, Color.White, Program.OnOnMissileCreateObjectBoundingRadius);
			}
			if (!string.IsNullOrWhiteSpace(Program.OnOnMissileCreateObjectPosition))
			{
				Drawing.DrawText(500f, 430f, Color.White, Program.OnOnMissileCreateObjectPosition);
			}
		}
		public static void DrawText(Vector3 position, float addPosX, float addPosY, Color color, string text, bool checkValue)
		{
			if (checkValue)
			{
				Vector2 vector = Drawing.WorldToScreen(position);
				Drawing.DrawText(vector.X + addPosX, vector.Y + addPosY, color, text);
			}
		}

		private static string OnOnMissileCreateObjectName;

		private static string OnOnMissileCreateObjectType;

		private static string OnOnMissileCreateObjectBoundingRadius;

		private static string OnOnMissileCreateObjectPosition;

		private static string OnDeleteObjectName;

		private static string OnDeleteObjectType;

		private static string OnDeleteObjectBoundingRadius;

		private static string OnDeleteObjectPosition;

		private static string OnCreateObjectName;

		private static string OnCreateObjectType;

		private static string OnCreateObjectBoundingRadius;

		private static string OnCreateObjectPosition;

		private static string Slot;

		private static string CastTime;

		private static string Time;

		private static string TotalTime;

		private static string CastRadius;

		private static string CastRange;

		private static string LineWidth;

		private static string LineDragLength;

		private static string MissileSpeed;

		private static string Name;

		private static string CastType;

		private static string MissileName;

		private static string MissilePosition;

		private static string MissileBoundingRadius;

		private static string MissileNetworkID;

		private static string MissileType;

		private static string OnDoCastSpellName;

		private static string OnDoCastSpellSpeed;

		private static string OnDoCastSpellCastRadius;

		private static string OnDoCastCastRange;

		private static string OnDoCastTime;

		private static string OnDoCastWidth;

		private static string MyBuffName;

		private static string MyBuffCount;

		private static string MyBuffType;

		private static string EnemyBuffName;

		private static string EnemyBuffCount;

		private static string EnemyBuffType;

		private static string MinionName;

		private static string MinionType;

		private static string MinionPosition;
	}
}
