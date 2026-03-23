export interface Resource {
    uid: string;
    fileName: string;
    extension: string;
    mimeType: string;
    sizeInBytes: number;
    createdAt: string;
    isThumbnail: boolean | null;
    fileUrl: string;
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
