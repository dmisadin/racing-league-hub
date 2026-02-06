export interface IEnumItem {
    Value: number,
    ValueName: string,
    Title: string
};

export interface IEnumArray extends Array<IEnumItem>{};

export interface IEnum {
    [key: string]: IEnumItem;
};