# About

## WORK IN PROGRESS!

The plan here is to create a program that can shut down services and executables before starting a program in a given priority.

## Uhm, why? There's plenty of programs that do this.

I know! But name one program that doesn't install a service itself or remains in memory?

This program creates a (tempoary) batch file to launch the program you want then shuts down (if you want it to).

What LaunchIt can do is determine what was started while you launched another program. Cause here's the thing. Windows tends to startup servcices and executables when it thinks it needs them.

So first you're wasting time shutting things down, then you're even unaware 'windows known bestter' when it restarts something.