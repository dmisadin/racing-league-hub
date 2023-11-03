const fs = require("fs");
const { F1TelemetryClient, constants } = require('@racehub-io/f1-telemetry-client');
const { PACKETS } = constants;

const client = new F1TelemetryClient({ port: 20777 });

BigInt.prototype.toJSON = function () { return this.toString() } //Because of issues with JSON.stringify()

// to start listening:
client.start();

writeFinalClassificationToFile = (data) => {
    const currentDateTime = new Date();
    const currentDate = currentDateTime.toISOString().slice(0, 10);
    const currentTime = currentDateTime.toTimeString().slice(0, 8).replaceAll(':', '-');
    const filepath = `./records/${currentDate}-${currentTime}.json`;

    fs.writeFile(filepath, JSON.stringify(data), (err) => {
        if (err)
            return console.log(err);
        console.log("The file was saved at: ", filepath);
    });

}

client.on(PACKETS.finalClassification, writeFinalClassificationToFile);

// and when you want to stop:
//client.stop();