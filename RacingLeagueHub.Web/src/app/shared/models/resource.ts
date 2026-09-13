import { BaseDto } from "./dtos";

export interface ResourceDto extends BaseDto {
    storageId: string;
    fileUrl: string;
    extension: string;
    fileName: string;
    mimeType: string;
    sizeInBytes: number;
    createdAt: string;
    isThumbnail: boolean | null;
}

export interface CreateResourceRequest {
    fileName: string;
    mimeType: string;
    sizeInBytes: number;
    isThumbnail?: boolean | null;
}

export interface UploadUrlResponse {
    uid: string;
    uploadUrl: string;
}
