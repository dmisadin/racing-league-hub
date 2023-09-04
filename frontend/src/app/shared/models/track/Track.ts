export class Track {
    id: number = 0;
    name: string = "Albert Park Circuit";
    cornersTotal: number = 14;
    cornersLeft: number = 6;
    elevation: number = 2.4;
    length: number = 5278;
    pitStop?: number | null = 25;
    imagePath?: string | null;
    countryId: number = 10;
    location: string = "Melbourne";
}