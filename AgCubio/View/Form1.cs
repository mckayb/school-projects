using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgCubio;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Holds the current world info
        /// </summary>
        private World worldInfo = new World(1000, 1000, new Dictionary<String, Cube>());

        /// <summary>
        /// Holds the socket we are connecting on
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Keep track of our font size.
        /// </summary>
        private const int fontSize = 20;

        /// <summary>
        /// The brush used to paint the cubes on the page.
        /// </summary>
        private SolidBrush myBrush;

        /// <summary>
        /// Our Preserved State Object
        /// </summary>
        private Preserved_Socket_State ps = new Preserved_Socket_State();

        /// <summary>
        /// Delegate for handling our PreservedState.
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        private delegate void actionCallback(Preserved_Socket_State ps);

        /// <summary>
        /// Keeps track of the current player cube x coordinate.
        /// </summary>
        private float playerCubeLocX;

        /// <summary>
        /// Keeps track of the current player cube y coordinate.
        /// </summary>
        private float playerCubeLocY;

        /// <summary>
        /// Keeps track of the current frameRate
        /// </summary>
        private static int frameRate;

        /// <summary>
        /// Keeps track of what the last frameRate was
        /// </summary>
        private static int lastFrameRate;

        /// <summary>
        /// Keeps track of the last tick, useful in calculating the frameRate
        /// </summary>
        private static int lastTick;

        /// <summary>
        /// Unique identifier for the player cube.
        /// </summary>
        private string playerCubeUid;

        /// <summary>
        /// Timer for helping connect timeouts.
        /// </summary>
        private System.Timers.Timer connectTimout;

        /// <summary>
        /// Font for the text on the player cubes.
        /// </summary>
        private Font font1 = new Font("Quartz MS", fontSize, FontStyle.Bold, GraphicsUnit.Point);

        /// <summary>
        /// UTF8 encoding for data processing
        /// </summary>
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// String Format for drawing cubes
        /// </summary>
        private StringFormat df = new StringFormat();

        /// <summary>
        /// Delegate useful in updating the stats.
        /// </summary>
        delegate void updateStats();

        /// <summary>
        /// Delegate useful in updating the gui intro panel
        /// </summary>
        /// <param name="visible"></param>
        delegate void updateGUICallback(bool visible);

        /// <summary>
        /// Whether we have receieved the player cube or not.
        /// </summary>
        private bool hasPlayerCube;

        /// <summary>
        /// Constructor for our form. Shows the intro panel on start up.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.BackColor = Color.White;
            this.FormClosing += Form1_FormClosing;
            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
            serverName.KeyDown += ServerName_KeyDown;
            playerName.KeyDown += PlayerName_KeyDown;
            df.Alignment = StringAlignment.Center;
            df.LineAlignment = StringAlignment.Center;

        }

        /// <summary>
        /// Paint Method for the Form. Calls this every time we get more data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (hasPlayerCube)
            {
                if (socket.Connected)
                {
                    CalculateFrameRate();
                    e.Graphics.Clear(Color.White);
                    lock (worldInfo)
                    {
                        Dictionary<String, Cube> cubes = this.worldInfo.GetCubes();

                        if (cubes.ContainsKey(playerCubeUid))
                        {
                            Cube playerCube = cubes[playerCubeUid];
                            int viewportWidth = Screen.PrimaryScreen.Bounds.Width;
                            float scale = viewportWidth / (playerCube.width * 5);
                            playerCubeLocX = playerCube.left * scale;
                            playerCubeLocY = playerCube.bottom * scale;
                            foreach (KeyValuePair<String, Cube> kv in cubes)
                            {
                                Cube cube = kv.Value;
                                Color color = Color.FromArgb(cube.argb_color);
                                myBrush = new SolidBrush(color);
                                float x = (cube.left * scale) - playerCubeLocX + (IntroPanel.Bounds.Width / 2) - (playerCube.width / 2);
                                float y = (cube.bottom * scale) - playerCubeLocY + (IntroPanel.Bounds.Height / 2) - (playerCube.width / 2);
                                float w = cube.width * scale;
                                float h = w;

                                RectangleF rec = new RectangleF(x, y, w, h);


                                e.Graphics.FillRectangle(myBrush, rec);
                                e.Graphics.DrawString(cube.name, font1, Brushes.White, rec, df);
                            }
                        }
                        else
                        {
                            // Disconnect Gracefully and show the Intro Panel again.
                            String stats = "Game Statistics: \n";
                            stats += massLabel.Text + "\n";
                            stats += widthLabel.Text + "\n";
                            stats += foodLabel.Text + "\n";
                            MessageBox.Show(stats);
                            Network.Disconnect(socket);
                            hasPlayerCube = false;
                            base.BeginInvoke(new MethodInvoker(() => updatePanel(true)));
                        }
                    }
                    base.BeginInvoke(new MethodInvoker(() => sendMove()));
                    base.BeginInvoke(new MethodInvoker(() => updateStatsFunc()));
                }
            }
        }

        /// <summary>
        /// Form Closing Event method. Disconnects the socket when they're closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( socket != null && socket.Connected )
            {
                Network.Disconnect(socket);
                hasPlayerCube = false;
            }
        }

        /// <summary>
        /// Makes sure they have entered a server and a player name before connecting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (serverName.Text == "")
                {
                    MessageBox.Show("Please enter a server.");
                    return;
                }

                if (playerName.Text == "")
                {
                    MessageBox.Show("Please enter a player name.");
                    return;
                }
                connectButton.PerformClick();
            }
        }

        /// <summary>
        /// Makes sure they have entered a server and a player name before connecting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (serverName.Text == "")
                {
                    MessageBox.Show("Please enter a server.");
                    return;
                }

                if (playerName.Text == "")
                {
                    MessageBox.Show("Please enter a player name.");
                    return;
                }
                connectButton.PerformClick();
            }
        }

        /// <summary>
        /// Handler for when the user presses the space bar key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (socket != null && socket.Connected && e.KeyCode == Keys.Space)
            {
                string splitCommand = "(split, " + (int)playerCubeLocX + ", " + (int)playerCubeLocY + ")\n";
                Network.Send(socket, splitCommand);
            }
        }

        /// <summary>
        /// Handler for when the user clicks the connect button to connect to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "Connecting...";

            connectTimout = new System.Timers.Timer(3000);
            connectTimout.Elapsed += ConnectTimout_Elapsed;
            connectTimout.Start();

            actionCallback handler = connectCallbackFunc;
            socket = Network.Connect_to_Server(handler, serverName.Text);

        }

        /// <summary>
        /// Function that gets called after 3 seconds of not being able to connect properly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectTimout_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (socket == null || !socket.Connected)
            {
                errorLabel.Text = "There were problems with your connection.";
                connectTimout.Stop();
                connectTimout.Dispose();
                socket.Close();
            }
        }

        /// <summary>
        /// Callback for when the user first connects to the server.
        /// Sends the player name off to the server.
        /// Sets the new callback to be for when we receive data.
        /// </summary>
        /// <param name="state"></param>
        private void connectCallbackFunc(Preserved_Socket_State state)
        {
            base.BeginInvoke(new MethodInvoker(() => updatePanel(false)));

            actionCallback handler = receivePlayerFunc;
            state.callback = handler;

            string name = playerName.Text + '\n';
            Network.Send(state.workSocket, name);
        }




        /// <summary>
        /// Callback that happens when we receive the player data from the
        /// server.
        /// </summary>
        /// <param name="state"></param>
        private void receivePlayerFunc(Preserved_Socket_State state)
        {
            lock (worldInfo)
            {
                actionCallback handler = receiveDataFunc;
                state.callback = handler;

                state.sb.Append(encoding.GetString(state.buffer, 0, state.bytesRead));
                string response = state.sb.ToString();

                Cube playerCube = JsonConvert.DeserializeObject<Cube>(response);
                playerCubeUid = playerCube.uid;

                worldInfo.addCube(playerCube);
                hasPlayerCube = true;

                this.Invalidate();

                Network.i_want_more_data(state);
            }
        }

        /// <summary>
        /// Callback for when the user receieves data from the server.
        /// Sets initial player cube if we don't have it.
        /// Otherwise it sets all of the new cubes in the world.
        /// Asks for more data from the server.
        /// </summary>
        /// <param name="state"></param>
        private void receiveDataFunc(Preserved_Socket_State state)
        {
            lock (worldInfo)
            {

                state.sb.Append(encoding.GetString(state.buffer, 0, state.bytesRead));
                string response = state.sb.ToString();
                string lastCube = worldInfo.updateCubes(response);
                state.sb.Replace(response, lastCube);
                this.Invalidate();

                Network.i_want_more_data(state);
            }
        }

        /// <summary>
        /// Function to send our move command to the client.
        /// Gets called every time it paints.
        /// </summary>
        private void sendMove()
        {
            lock( worldInfo )
            {

                Cube playerCube = worldInfo.getCubeById(playerCubeUid);
                Point mousePos = base.PointToClient(Control.MousePosition);
                float x = mousePos.X;
                float y = mousePos.Y;
                string moveCommand = "(move, " + (int)mousePos.X + ", " + (int)mousePos.Y + ")\n";
                Network.Send(socket, moveCommand);
            }
        }

        /// <summary>
        /// Function to update the visibility of our intro panel.
        /// Gets called when we die, and when we successfully connect.
        /// </summary>
        /// <param name="visible"></param>
        private void updatePanel(bool visible)
        {
            if ( IntroPanel.InvokeRequired )
            {
                updateGUICallback c = updatePanel;
                this.Invoke(c, new object[] { visible });
            } else
            {
                IntroPanel.Visible = visible;
            }
        }

        /// <summary>
        /// Function to update the current stats of the game.
        /// Is updated on every paint method.
        /// </summary>
        private void updateStatsFunc()
        {
            if (frameRateLabel.InvokeRequired)
            {
                updateStats fr = updateStatsFunc;
                this.Invoke(fr, new object[] { });
            }
            else
            {
                lock( worldInfo )
                {
                    Cube playerCube = worldInfo.getCubeById(playerCubeUid);
                    if (playerCube != null)
                    {
                        frameRateLabel.Text = "FPS: " + lastFrameRate;
                        massLabel.Text = "Mass: " + (int)playerCube.mass;
                        widthLabel.Text = "Width: " + (int)playerCube.width;
                        foodLabel.Text = "Food: " + worldInfo.getFoodCount();
                    }
                }
            }

        }
        /// <summary>
        /// Method for calculating the current FPS of the application.
        /// Updates the frameRateLabel text to show the last frameRate.
        /// </summary>
        private void CalculateFrameRate()
        {
            if (Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = Environment.TickCount;
            }
            frameRate++;
        }
    }
}
