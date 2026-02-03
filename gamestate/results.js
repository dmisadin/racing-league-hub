const fs = require("fs");
const { F1TelemetryClient, constants } = require('@racehub-io/f1-telemetry-client');
const { PACKETS } = constants;
const client = new F1TelemetryClient({ port: 20777 });
BigInt.prototype.toJSON = function () { return this.toString() } //Because of issues with JSON.stringify()

// to start listening:
client.start();

const writeFinalClassificationToFile = (rawData) => {
    const currentDateTime = new Date();
    const currentDate = currentDateTime.toISOString().slice(0, 10);
    const currentTime = currentDateTime.toTimeString().slice(0, 8).replaceAll(':', '-');
    const filepath = `./records/${currentDate}-${currentTime}.csv`;
    const data = formatToCSV(rawData.m_classificationData)
    console.log(data)
    fs.writeFile(filepath, data, (err) => {
        if (err)
            return console.log(err);
        console.log("The file was saved at: ", filepath);
    });
}

client.on(PACKETS.finalClassification, writeFinalClassificationToFile);

const tyreEnum = Object.freeze({
    7: 'i',
    8: 'w',
    16: 's',
    17: 'm',
    18: 'h'
});
const DNF = "DNF";

var _counterDNF = 0;

const formatToCSV = (results) => {
    results.sort(descendingPositions);

    var resultsString = "";
    const separator = ',';

    //header row
    resultsString += "m_position,m_totalRaceTime,,,m_bestLapTimeInMS,,Tyre 1,Tyre 2,Tyre 3,Tyre 4,Tyre 5,Tyre 6,Tyre 7,Tyre 8,m_penaltiesTime\n";
    const firstPlace = results.find(r => r.m_position == 1); // To do: null handling
    const firstPlaceTime = firstPlace.m_totalRaceTime + firstPlace.m_penaltiesTime;
    const lapsInRace = firstPlace.m_numLaps;

    results.forEach((r, i) => {
        if(!r.m_position) return;

        resultsString += `${r.m_position}${separator}`;
        resultsString += `${getRaceTime(r.m_totalRaceTime + r.m_penaltiesTime, r.m_position, firstPlaceTime, lapsInRace, r.m_numLaps)}${separator}`;
        resultsString += separator; // Empty column for calculating
        resultsString += separator; // Empty column for stewards penalties
        resultsString += `${formatMilisecodsToTime(r.m_bestLapTimeInMS).substring(1)}${separator}`;
        resultsString += separator; // Empty column for qualifying tyres
        r.m_tyreStintsVisual.forEach(tyre => {
            if(!tyre && tyre != 7 && tyre != 8 && tyre != 16 && tyre != 17 && tyre != 18)
                resultsString += separator;
            else 
                resultsString += `${tyreEnum[tyre]}${separator}`;
        });
        if (r.m_penaltiesTime)
            resultsString += `'+${r.m_penaltiesTime} sek`;
        
        resultsString += separator; // comma for m_penaltiesTime
        resultsString += "\n";
    });

    _counterDNF = 0;

    return resultsString;
}

const getRaceTime = (time, position, firstPlaceTime, lapsInRace, lapsCompleted) => {
    if (position == 1)
        return formatSecodsToTime(time);

    const timeDifferenceToFirstPlace = time - firstPlaceTime;
    if (timeDifferenceToFirstPlace < 0) {
        _counterDNF++;
        return `${DNF} ${_counterDNF}`;
    }

    const lapsCompletedDifference = lapsInRace - lapsCompleted;
    const plural = lapsCompletedDifference > 1 ? 'A' : '';
    if (lapsCompletedDifference > 0)
        return `'+${lapsCompletedDifference} KRUG${plural}`;

    return formatSecodsToTime(timeDifferenceToFirstPlace);
}

const descendingPositions = (a, b) => a.m_position - b.m_position;

const formatMilisecodsToTime = (miliseconds) => {
    miliseconds = parseFloat(miliseconds);
    return formatSecodsToTime(miliseconds / 1000);
};
const formatSecodsToTime = (value) => {
    const miliseconds = value * 1000; // Convert to integer (miliseconds)
    let minutes = Math.floor(value / 60);
    const seconds = (miliseconds % 60000) / 1000;
    const hours = Math.floor(minutes / 60)
    minutes %= 60;
    let result = '';

    if (hours)
        result += hours + ':';
    if (minutes)
        result += ((minutes < 10 && hours) ? '0' : '') + minutes + ':';
    else
        result += (hours ? '00:' : '');

    result += ((seconds < 10 && minutes) ? '0' : '') + seconds.toFixed(3);

    return formatToAdria(result);
};

const formatToAdria = (value) => {
    return value.replace(/[^0-9]/g, '');
};

//var resultData = require('./records/2023-11-14-22-00-35.json');
//writeFinalClassificationToFile(resultData);
// and when you want to stop listening:
//client.stop();