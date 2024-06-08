import { IEnum } from "../interfaces";

export const ResultStatus: IEnum = {
    Invalid: {
        Value: 0,
        ValueName: "Invalid",
        Title: "Invalid"
    },
    Inactive: {
        Value: 1,
        ValueName: "Inactive",
        Title: "Inactive"
    },
    Active: {
        Value: 2,
        ValueName: "Active",
        Title: "Active"
    },
    Finished: {
        Value: 3,
        ValueName: "Finished",
        Title: "Finished"
    },
    DNF: {
        Value: 4,
        ValueName: "DNF",
        Title: "Did not finish"
    },
    DSQ: {
        Value: 5,
        ValueName: "DSQ",
        Title: "Disqualified"
    },
    NC: {
        Value: 6,
        ValueName: "NC",
        Title: "Not classified"
    },
    Retired: {
        Value: 7,
        ValueName: "Retired",
        Title: "Retired"
    }
}

export const Game: IEnum = {
    F12010: {
        Value: 0,
        ValueName: "F12010",
        Title: "F1 2010"
    },
    F12011: {
        Value: 1,
        ValueName: "F12011",
        Title: "F1 2011"
    },
    F12012: {
        Value: 2,
        ValueName: "F12012",
        Title: "F1 2012"
    },
    F12013: {
        Value: 3,
        ValueName: "F12013",
        Title: "F1 2013"
    },
    F12014: {
        Value: 4,
        ValueName: "F12014",
        Title: "F1 2014"
    },
    F12015: {
        Value: 5,
        ValueName: "F12015",
        Title: "F1 2015"
    },
    F12016: {
        Value: 6,
        ValueName: "F12016",
        Title: "F1 2016"
    },
    F12017: {
        Value: 7,
        ValueName: "F12017",
        Title: "F1 2017"
    },
    F12018: {
        Value: 8,
        ValueName: "F12018",
        Title: "F1 2018"
    },
    F12019: {
        Value: 9,
        ValueName: "F12019",
        Title: "F1 2019"
    },
    F12020: {
        Value: 10,
        ValueName: "F12020",
        Title: "F1 2020"
    },
    F12021: {
        Value: 11,
        ValueName: "F12021",
        Title: "F1 2021"
    },
    F122: {
        Value: 12,
        ValueName: "F122",
        Title: "F1 22"
    },
    F123: {
        Value: 13,
        ValueName: "F123",
        Title: "F1 23"
    },
    F124: {
        Value: 14,
        ValueName: "F124",
        Title: "F1 24"
    }
}

export const Platform: IEnum = {
    Steam: {
        Value: 1,
        ValueName: "Steam",
        Title: "Steam"
    },
    PlayStation: {
        Value: 2,
        ValueName: "PlayStation",
        Title: "PlayStation"
    },
    Xbox: {
        Value: 3,
        ValueName: "Xbox",
        Title: "Xbox"
    },
    Origin: {
        Value: 4,
        ValueName: "Origin",
        Title: "EA"
    },
    Crossplay: {
        Value: 5,
        ValueName: "Crossplay",
        Title: "Crossplay"
    }
}

export const Region: IEnum = {
    Adria: {
        Value: 1,
        ValueName: "Adria",
        Title: "Adria"
    },
    Europe: {
        Value: 2,
        ValueName: "Europe",
        Title: "Europe"
    },
    NorthAmerica: {
        Value: 3,
        ValueName: "NorthAmerica",
        Title: "North America"
    },
    SouthAmerica: {
        Value: 4,
        ValueName: "SouthAmerica",
        Title: "South America"
    },
    Asia: {
        Value: 5,
        ValueName: "Asia",
        Title: "Asia"
    },
    Oceania: {
        Value: 6,
        ValueName: "Oceania",
        Title: "Oceania"
    },
    Africa: {
        Value: 7,
        ValueName: "Africa",
        Title: "Africa"
    }
}

export const PointsType: IEnum = {
    Race: {
        Value: 1,
        ValueName: "Race",
        Title: "Race"
    },
    Sprint: {
        Value: 2,
        ValueName: "Sprint",
        Title: "Sprint"
    },
    Qualifying: {
        Value: 3,
        ValueName: "Qualifying",
        Title: "Qualifying"
    },
    FastestLap: {
        Value: 4,
        ValueName: "FastestLap",
        Title: "Fastest Lap"
    }
}


export const SessionType: IEnum = {
    Practice: {
        Value: 1,
        ValueName: "Practice",
        Title: "Practice"
    },
    Qualifying: {
        Value: 2,
        ValueName: "Qualifying",
        Title: "Qualifying"
    },
    Sprint: {
        Value: 3,
        ValueName: "Sprint",
        Title: "Sprint"
    },
    Race: {
        Value: 4,
        ValueName: "Race",
        Title: "Race"
    },
}


/** Enumeration template
export const Name: IEnum = {
    Property: {
        Value: ,
        ValueName: "",
        Title: ""
    },
}
 */