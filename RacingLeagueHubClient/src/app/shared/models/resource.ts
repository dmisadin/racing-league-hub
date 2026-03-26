import { BaseDto } from "./dtos";

export interface ResourceBaseDto extends BaseDto {
    fileUrl: string;
}

export interface ResourceDto extends ResourceBaseDto {
    storageId: string;
    fileName: string;
    extension: string;
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
