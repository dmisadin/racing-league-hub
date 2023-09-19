import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'urlHttp'
})
export class UrlHttpPipe implements PipeTransform {

	transform(url: string): string {
		return url.startsWith("http://") || url.startsWith("https://") ? url : "http://" + url;
	}

}
