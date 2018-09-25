# Email plug

The Email plug provides an interface to notify users through a regular email server.

This is the source code used by the actual built-in Email plug. Use it as a reference
to build your own CI plug!

# Build
The executable is built from .NET Framework code using the provided `src/emailplug.sln`
solution file. You can use Visual Studio or MSBuild to compile it.

**Note:** We'll use `${DEVOPS_DIR}` as alias for `%PROGRAMFILES%\PlasticSCM5\server\devops`
in *Windows* or `/var/lib/plasticscm/devops` in *macOS* or *Linux*.

# Setup
If you just want to use the built-in Email plug you don't need to do any of this.
The Email plug is available as a built-in plug in the DevOps section of the WebAdmin.
Open it up and configure your own!

## Configuration files
You'll notice some configuration files under `/src/configuration`. Here's what they do:
* `emailplug.log.conf`: log4net configuration. The output log file is specified here. This file should be in the binaries output directory.
* `notifier-email.definition.conf`: plug definition file. You'll need to place this file in the Plastic SCM DevOps directory to allow the system to discover your Email plug.
* `emailplug.config.template`: mergebot configuration template. It describes the expected format of the Email plug configuration. We recommend to keep it in the binaries output directory
* `emailplug.conf`: an example of a valid Email plug configuration. It's built according to the `emailplug.config.template` specification.

## Add to Plastic SCM Server DevOps
To allow Plastic SCM Server DevOps to discover your custom Email plug, just drop 
the `notifier-email.definition.conf` file in `${DEVOPS_DIR}/config/plugs/available`.
Make sure the `command` and `template` keys contain the appropriate values for
your deployment!

# Behavior
The **Email plug** provides an API for **mergebots** to connect to a SMTP server
and send messages to developers with it.

## What the configuration looks like
When a mergebot requires a Notifier plug to work, you can select a Email Plug Configuration.

<p align="center">
  <img alt="Issue Tracker plug select" src="https://raw.githubusercontent.com/mig42/emailplug/master/doc/img/notifier-plug-select.png" />
</p>

You can either select an existing configuration or create a new one.

When you create a new Email Plug Configuration, you have to fill in the following values:

<p align="center">
  <img alt="Emailplug configuration example"
       src="https://raw.githubusercontent.com/mig42/emailplug/master/doc/img/configuration-example.png" />
</p>

## Email Configuration

You'll need to make sure your email provider allows you to use their service as a
SMTP server. For instance, here are the instructions for [GMail](https://support.google.com/a/answer/176600?hl=en)
and [Outlook](https://support.office.com/en-us/article/pop-imap-and-smtp-settings-for-outlook-com-d088b986-291d-42b8-9564-9c414e2aa040)

# Support
If you have any questions about this plug don't hesitate to contact us by
[email](support@codicesoftware.com) or in our [forum](http://www.plasticscm.net)!
