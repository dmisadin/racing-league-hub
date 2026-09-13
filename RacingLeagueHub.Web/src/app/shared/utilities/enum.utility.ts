import { DropdownOption } from "../models/models";

export function enumToOptions(e: any): DropdownOption[] {
    return Object.entries(e)
        .filter(([_, v]) => typeof v === 'number')
        .map(([k, v]) => ({
            id: v as number,
            name: k
        }));
}