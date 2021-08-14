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
        * launchit -priority=high
* __PrerredEditor__
  * Default: notepad.exe
  * What it does
    * Allows you to configure your favorite editor for editing the configuration file.
  * Why
    * Notepad is everywhere but I personally prefer Notepad++. You might have your own preferences.
* __MonitoringInterval__
  * Default: 30000 milliseconds
  * What it does:
    * Sets the interval in milliseconds LaunchIt will check for started processes after your program is started.
* __Services__
  * Default: Nothing.
  * What it does
    * Allows you to specify which services you want LaunchIt to shutdown.
  * Command line switch
    * launchit -services=Service 1, Service 2
    * NOTE! LaunchIt will combine the services in your configuration file with any you specify using the command line switch.
      First it will stop services you specified in your configuration file, next, it will stop services you specified in the command line.
* __Executables__
  * Identical to services but here you specify which executables you want to stop.
  * What it does
    * Allows you to specify which executables you want Launchit to kill.
      * Yes 'kill'. This is akin to using the Task Manager to stop a program. This is not a nice way to stop
        A program and if you kill a running program and you have unsaved changes they will be lost.
  * Command line switch
    * launchit -executables=Exe 1.exe, Exe 2.exe


## Command line switches

LaunchIt supports several command line switches that allow you to customize how you want to run a program.

You configuration is your base, command line switches allow you to overrule that, or, for services and executables, add to it.

### Run

__Example__
> launchit -run=My Really Cool Program.exe

Tells LaunchIt what to run.

### Edit
> launchit -edit

Opens your launchit configuration file notepad or your preferred editor.

## UseBatch
> launchit -usebatch

Instructs LaunchIt to create a batch file to run your program.

## ShutdownExplorer
> launchit -shutdownexplorer

Instructs LaunchIt to shut down explorer. This command line switch is handly if you don't want to do this by default.

## Priority
> launchit priority=abovenormal

Allows you to specify which priority you want to run your program.

