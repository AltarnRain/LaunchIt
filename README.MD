# LaunchIt

LaunchIt is a program that shuts down anything you don't need before starting a program.

This can by anything but I wrote it with games in mind.

LaunchIt can run a program in two ways:
1. Diagnostic mode
1. Batchfile mode

You start with `Diagnostic mode` to test which services and executables can be shut down without them being restarted. 
In this mode LaunchIt stays active and checks periodically what Windows is doing while you're busy with other things.

Once you've figured out which executables/services can be shut down and stay shut down you switch to `Batchfile` mode. 
In this mode LaunchIt creates a batch file, runs it, and shuts itself down.

## Uhm, why? There's plenty of programs that do this.

Well... kinda...

### Razer Cortex.
I tried Razer Cortex and really, really liked what it tried to do. Unfortunately, it required a service itself and made and effect to restart things it shutdown.

That part I did not like so much, especially the need for yet another service on my PC while I was trying to elimate as many off them as I could.
In addition, Razer Cortex didn't support a lot of services to shut down either.

### My Batch file solution
I used a batch file with 'net stop' and 'taskkill' commands a long time but what I could do with such a batch file was limited.
For example: stubborn services and executables that restart! Arghh!

Try shutting down 'Windows Search' and see how quickly it is restarted. Very annoying.

But thanks to LaunchIt I found out the reboot of 'Windows search' was triggered by Explorer.exe. And that's just one example, there's plenty more.

Because of this LaunchIt has a configuration option to shutdown explorer.exe. 

## Configuration file

You can edit the configuration file with the following command:

> launchit -edit

This will open the configuration file in notepad.

Lets go over the settings:

* __ShutdownExplorer__
  * Default: true
  * What it does
    * Kills the `Explorer.exe` process. That's your UI.
  * Why
    * `Explorer.exe` will start other processes. When its shutdown it can't.
  * Commandline switch
    * launchit -shutdownexplorer
* __Priority__
  * Default: AboveNormal
  * What it does
    * Sets the process priority of the program you want to start.
      * Valid options
        * Idle
        * BelowNormal
        * Normal
        * AboveNormal
        * High
        * RealTime
  * Why
    * The more attention a program gets, the smoother it runs. Might also increase the FPS of a game.
    * Careful with this setting! Not every program 'likes' it to run with a high or lower priority.
    * Commandline switches:
      * For example: 
        * launchit -high
* PrerredEditor
  * Default: notepad.exe
  * What it does
    * Allows you to configure your favorite editor for editing the configuration file.
  * Why
    * Notepad is everywhere but I personally prefer Notepad++.
* MonitoringInterval
  * Default: 30000 milliseconds
  * What it does:
    * Sets the interval in milliseconds LaunchIt will check for started processes after your program is started.

    
# List services you wish to shut down by their full name in this section. For example: - My Service
Services:
- Example Service 1
- Example Service 2
# List executables you wish to shut down. For example: - MyExecutable
Executables:
- Example executable 1
- Example executable 2
```
