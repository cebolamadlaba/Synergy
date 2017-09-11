# Scheduler Service
### Tech

Scheduler service uses a number of open source projects to work properly:

* [Hangfire] - An easy way to perform background processing in .NET and .NET Core applications!


### Installation
```sh
$ dotnet publish
$ copy output files to IIS site
$ ensure that the app pool used is set not never sleep, set the Idle Time-out to `0`
```
### Adding Jobs
* Create a new class that implements `IJob` interface , set the `Name` , `JobType` .The `Run()` is the actual work the job must perform.
* For `OnceOff` jobs the `OnceOffRunAt` must be set.
* For `RecurringJobs` the `Cron` must be set.
** Cron is the linux cron which `Hangfire.Cron` implements for the most common cron expressions like `Hangfire.Cron.Daily()`.
* That is all that is needed to add new jobs `Autofac` will take care of finding the jobs so the can be scheduled by `Startup.ScheduleJobs()`.

### Hangfire Dashboard

* To view the dashboard just navigate to site-url/`hangfire`





