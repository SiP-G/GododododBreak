using Godot;
using System;
using System.Collections.Generic;

namespace Godododod.Dungeon
{
	public class Level
	{
		public TileMap Walls { get; private set; }

		public TileMap Floor { get; private set; }

		private int _TileSize = 16;
		private Vector2 _Size;

		private int _ChunkSize;
		public List<Chunk> ChunkList = new List<Chunk>();

		private int _MinRoomSize;
		private int _MaxRoomSize;
		private int _NumRooms;
		private List<Vector2> _RoomsPos = new List<Vector2>();

		private Random _Rand = new Random();

		public Level(Vector2 size, int chunkSize=20, int minRoomSize=5, int maxRoomSize=16, int numRooms=10)
		{
			_Size = size;
			_ChunkSize = chunkSize;
			_MinRoomSize = minRoomSize;
			_MaxRoomSize = maxRoomSize;
			_NumRooms = numRooms;

			Walls = new TileMap();
			Floor = new TileMap();

			Floor.CellSize = new Vector2(_TileSize, _TileSize);
			Walls.CellSize = new Vector2(_TileSize, _TileSize);
			Walls.CellTileOrigin = TileMap.TileOrigin.Center;
			Walls.CellYSort = true;

			Floor.TileSet = (TileSet)ResourceLoader.Load("res://Resources/TileSets/Floor.tres");
			Walls.TileSet = (TileSet)ResourceLoader.Load("res://Resources/TileSets/Walls.tres");

			GenerateLevels();
		}

		private void GenerateLevels()
		{
			FillArea(Walls, Vector2.Zero, _Size, 0);
			CreateChunk();
			ShuffleChunks();
		}

		private void FillArea(TileMap tileMap, Vector2 pos, Vector2 size, int idTile)
		{
			Vector2 tilePos;

			for (int y=0;y<size.y;y++)
			{
				for (int x=0;x<size.x;x++)
				{
					tilePos.x = pos.x + x;
					tilePos.y = pos.y + y;
					tileMap.SetCellv(tilePos, idTile);
				}
			}
			tileMap.UpdateBitmaskRegion();
		}

		public void ConnectLevel(Node parent)
		{
			parent.AddChild(Walls);
		}

		private void CreateChunk()
		{
			Vector2 chunkPos = Vector2.Zero;

			for (int chunkY = 0; chunkY < _Size.y / _ChunkSize; chunkY++)
			{
				for (int chunkX = 0; chunkX < _Size.x / _ChunkSize; chunkX++)
				{
					chunkPos.x = chunkX * _ChunkSize;
					chunkPos.y = chunkY * _ChunkSize;

					ChunkList.Add(new Chunk(chunkPos)); 
				}
			}
		}

		private void ShuffleChunks()
		{
			for (int i = ChunkList.Count - 1; i >= 1; i--)
			{
				int j = _Rand.Next(i + 1);

				Chunk currentChunk = ChunkList[j];
				ChunkList[j] = ChunkList[i];
				ChunkList[i] = currentChunk;
			}
		}

		private void GenerateRooms()
		{
			for ( int i = 0; i < _NumRooms; i++)
			{
				Room room = new Room(Walls, ChunkList[i].Position, _ChunkSize, _MinRoomSize, _MaxRoomSize);
				ChunkList[i].Room = room;
				FillArea(Floor, room.Position, new Vector2(room.Width, room.Height),0);
				_RoomsPos.Add(room.Center);
			}
		}
	}
}
