import { DropdownOption } from "../models/models";

export function getUtcOffsetMinutes(tz: string): number {
    const now = new Date();
    const utcDate = new Date(now.toLocaleString('en-US', { timeZone: 'UTC' }));
    const tzDate = new Date(now.toLocaleString('en-US', { timeZone: tz }));
    return (tzDate.getTime() - utcDate.getTime()) / 60_000;
}

export const timezoneOptions: DropdownOption[] = Intl.supportedValuesOf('timeZone')
    .map(tz => {
        const offset = getUtcOffsetMinutes(tz);
        const sign = offset >= 0 ? '+' : '-';
        const abs = Math.abs(offset);
        const h = String(Math.floor(abs / 60)).padStart(2, '0');
        const m = String(abs % 60).padStart(2, '0');
        return {
            id: tz,
            name: `(UTC${sign}${h}:${m}) ${tz.replace(/_/g, ' ')}`,
            offset,
        };
    })
    .sort((a, b) => a.offset - b.offset || a.id.localeCompare(b.id))
    .map(({ id, name }) => ({ id, name }));

export function toUtcIso(localDateString: string): string {
    return new Date(localDateString).toISOString();
}