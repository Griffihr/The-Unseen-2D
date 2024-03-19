using Godot;
using System;

public partial class Menus : Node
{
	[Export]
	private int Port = 8910;

	[Export]
	private string Ip_Adress = "172.25.88.35";


	public int Max_Players = 10;

	private Control _Connect;
	private Control _Lobby;
	private OptionButton _Optionbutton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Connect = GetNode<Control>("Connect");
		_Lobby = GetNode<Control>("Lobby");

		_Optionbutton = GetNode<OptionButton>("Lobby/OptionButton");

		_Lobby.Hide();

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
		RpcId(1, "SendPlayerInformation", _Connect.GetNode<LineEdit>("LineEdit").Text , Multiplayer.GetUniqueId());
	}

	public void ConnectionFailed() {
		GD.Print("Failed to Connect");
	}

	public void StartServer() 
	{
		var _Port = int.Parse(_Connect.GetNode<LineEdit>("Port").Text);

		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();

		var Error = Peer.CreateServer(_Port, 2);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
			return;
		}

		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = Peer;

		GD.Print("Server Started");

		SendPlayerInformation(_Connect.GetNode<LineEdit>("PlayerName").Text, 1);

		_Connect.Hide();
		_Lobby.Show();

	}

	public void JoinServer() {
		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
		var Error = Peer.CreateClient(Ip_Adress, Port);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
			return;
		}
		
		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		
		Multiplayer.MultiplayerPeer = Peer;

		_Connect.Hide();
		_Lobby.Show();
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

		_Lobby.Hide();

		var Scene = GD.Load<PackedScene>("res://Map.tscn");	
		var _Scene = Scene.Instantiate<Node>();
		this.GetParent().AddChild(_Scene);

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
