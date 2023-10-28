const express = require("express");
const fs = require("fs");
const { F1TelemetryClient, constants } = require('@racehub-io/f1-telemetry-client');
const { PACKETS } = constants;

// App setup
const PORT = 1950;
const app = express();
const server = app.listen(PORT, function () {
    console.log(`\n\tListening on port ${PORT}`);
    console.log(`\thttp://localhost:${PORT}`);
});

const client = new F1TelemetryClient({ port: 20777 });

BigInt.prototype.toJSON = function() { return this.toString() }

client.start();
writeFinalClassificationToFile = (data) => {
    console.log(data);
    const currentDateTime = new Date();
    let currentDate = currentDateTime.toISOString().slice(0,10).replaceAll(':', '-');
    let currentTime = currentDateTime.toTimeString().slice(0, 8).replaceAll(':', '-');
    fs.writeFile(`./records/${currentDate}-${currentTime}.json`, JSON.stringify(data), (err) => {
        if (err)
            return console.log(err);
        console.log("The file was saved!");
    });

}
// to start listening:
client.on(PACKETS.finalClassification, writeFinalClassificationToFile);
// and when you want to stop:
//client.stop();