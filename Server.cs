using Godot;
using System;

public partial class Server : Node
{
	[Export]
	private int Port = 8910;

	[Export]
	private string Ip_Adress = "172.25.88.35";


	public int Max_Players = 10;

	private Control _Control;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Control = GetNode<Control>("MainMenu");

		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
	}

	public void PeerConnected(long id) {
		GD.Print("Player Connected" + id.ToString());
	}

	public void PeerDisconnected(long id) {
		GD.Print("Player disconnected" + id.ToString());
	}

	public void ConnectedToServer() {
		GD.Print("Connected");
		RpcId(1, "SendPlayerInformation", _Control.GetNode<LineEdit>("LineEdit").Text , Multiplayer.GetUniqueId());
	}

	public void ConnectionFailed() {
		GD.Print("Failed to Connect");
	}

	public void StartServer() 
	{
		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
		var Error = Peer.CreateServer(Port, 2);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
		}

		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		Multiplayer.MultiplayerPeer = Peer;

		GD.Print("Server Started");

		SendPlayerInformation(_Control.GetNode<LineEdit>("LineEdit").Text, 1);
	}

	public void JoinServer() {
		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
		var Error = Peer.CreateClient(Ip_Adress, Port);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
		}
		
		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		
		Multiplayer.MultiplayerPeer = Peer;
	}

	
	public void StartGameButton() {
		Rpc("StartGame");
	}



	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	public void StartGame() {
		
		foreach (var item in GameManager.Players) 
		{
			GD.Print(item.Name + "Is Playing");
		}

		var Scene = GD.Load<PackedScene>("res://Map.tscn");	
		var _Scene = Scene.Instantiate<Node>();

		this.GetParent().AddChild(_Scene);

		_Control.Hide();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	public void SendPlayerInformation(string name, int id) {
		PlayerInfo _PlayerInfo = new PlayerInfo() {
			Name = name,
			Id = id
		};
		if (!GameManager.Players.Contains(_PlayerInfo)) {
			GameManager.Players.Add(_PlayerInfo);
		}
		
		if (Multiplayer.IsServer()) {
			foreach (var Item in GameManager.Players) 
			{
				Rpc("SendPlayerInformation", Item.Name, Item.Id);
			}
		}
	}
}
