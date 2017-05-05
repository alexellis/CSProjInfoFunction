FROM alexellisio/msbuild:12-4.6.1

ADD https://ci.appveyor.com/api/buildjobs/njaqgby4wok9q3e8/artifacts/watchdog%2Fwatchdog.exe watchdog.exe
#COPY watchdog.exe .
COPY .\\Function\\bin\\Debug\\Function.exe .

ENV suppress_lock="true"

ENV fprocess=Function.exe

CMD ["watchdog.exe"]
