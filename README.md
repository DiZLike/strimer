A program for creating your own online radio.
The following functions are currently implemented:
- Broadcast to ICEcast servers;
- Encoding a stream using the OPUS codec;
- Reading and passing tags to the stream (but not to the server page);
- Track randomizer.

Launching the app:
Upon first launch, the application will identify your operating system and copy the necessary libraries,
depending on the architecture of your computer. Then you will need to follow a few steps:

- Select the playback device (it is recommended to select "0" so that the program does not play audio).
- Specify the sampling rate.
- Enter the IP address or the address of your server to which the broadcast will be performed.
- Specify the server port.
- Come up with a name for the broadcast link that will be created.
- Come up with a name for the broadcast.
- Select the broadcast genre.
- Enter the parameters corresponding to the selected encoder.
- The initial setup is complete. The program will close with a playlist error (so far this is normal).

Setting up a playlist:
Open the settings file "strimer.conf" in the application folder (path: <application folder>\config). Find the [Radio] section and enter the path to your playlist in the "radio.playlist" key.

Playlist file syntax:
Each new track is added from a new line in the format "track=<track path>?;".
