![http://i.imgur.com/ZEVGe94.png](http://i.imgur.com/ZEVGe94.png)

Better description to come when its not 4AM ;)

This is a project aimed towards making the process of upgrading/replacing the Operating System(OS) on a standard x86/x64 computer as simple, easy, and automated as possible.

The idea is pretty straight forward; Create a boot-able Flash drive, boot the target computer to said flash drive, press a button corresponding to the OS you want. After you press that button, my GUI will;

1) clear(diskpart) the target drive

2) create a new partition

3) mark it active

4) format NTFS

5) then unpack the pre-built OS on to the newly formatted drive

6) Write a BCD loader

7) And finally reboot


After a reboot the Flash drive will detect there is now an OS on the computer, and will attempt to boot to the hard drive. Windows then goes in to a fully automated Out of Box Experience(OOBE).

After OOBE is done you are presented with a fully functional computer ready for customization.

The OS that is deployed does need to be built/imaged/packaged/ placed on Flash drive. This alone can take quite a while depending on how many different images/versions you want. For now im aiming for a basic image of each OS the Microsoft offers (Most popular first, then I will try uncommon OSs like MS Bob).

I will look into making my images available for the public, at a later time.

On an almost weekly basis I am testing this software/deployment method. So far no issues.  This project will be Incorporated in my Remote F/R project.