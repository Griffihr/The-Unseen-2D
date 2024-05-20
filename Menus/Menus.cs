using Godot;
using System;

public partial class Menus : Node
{
	[Export]
	private int Port;

	[Export]
	private string Ip_Adress = "172.25.88.35";


	public int Max_Players = 10;

	private Control connect;
	private Control lobby;
	private OptionButton optionButton;
	private LineEdit port;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		connect = GetNode<Control>("Connect");
		lobby = GetNode<Control>("Lobby");

		optionButton = GetNode<OptionButton>("Lobby/OptionButton");

		port = connect.GetNode<LineEdit>("Port");
		port.Text = "8910";

		lobby.Hide();

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
		RpcId(1, "SendPlayerInformation", connect.GetNode<LineEdit>("PlayerName").Text , Multiplayer.GetUniqueId());
	}

	public void ConnectionFailed() {
		GD.Print("Failed to Connect");
	}

	public void StartServer() 
	{
		Port = int.Parse(port.Text);

		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();

		var Error = Peer.CreateServer(Port, 2);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
			return;
		}

		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = Peer;

		GD.Print("Server Started");

		SendPlayerInformation(connect.GetNode<LineEdit>("PlayerName").Text, 1);

		connect.Hide();
		lobby.Show();

	}

	public void JoinServer() {
		Port = int.Parse(port.Text);

		ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
		var Error = Peer.CreateClient(Ip_Adress, Port);
		if(Error != Error.Ok) {
			GD.Print("Error" + Error.ToString());
			return;
		}
		
		Peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		
		Multiplayer.MultiplayerPeer = Peer;

		connect.Hide();
		lobby.Show();
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

		lobby.Hide();

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
