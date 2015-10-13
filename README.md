# CRT-Client
Computer Repair Toolkit client

This is a simple command line tool that does all the "routine" tasks for computer repairs. It also has the option to
communicate with a central server to receive the commands to run and to store maintenance logs.

## Tasks included

- Get windows product key
- List installed antivirus/malware products
- List installed runtimes

## Planned tasks

- Get product keys for more software
- Generic software uninstall feature
- Detect installed e-mail software and get account information
- Convert mailbox from common email software to maildir format to migrate to other pc
- List and install windows updates
- Detect special windows updates (like KB3035583 that creates the windows 10 upgrade message)
- Set system OEM information (manufacturer)

## Screenshot

![Reporting](https://raw.githubusercontent.com/BrixIT/CRT-Client/master/reporting.png)

## Define the central server in Avahi

This is an example service file for Avahi to publish the central server on your network

```xml
<?xml version="1.0" standalone='no'?><!--*-nxml-*-->
<!DOCTYPE service-group SYSTEM "avahi-service.dtd">

<!-- See avahi.service(5) for more information about this configuration file -->

<service-group>

  <name replace-wildcards="yes">CRT Master server (%h)</name>

  <service>
    <type>_crt._tcp</type>
    <port>31337</port>
  </service>

</service-group>
```
