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
        Title: "Not Classified"
    },
    Retired: {
        Value: 7,
        ValueName: "Retired",
        Title: "Retired"
    }
}
