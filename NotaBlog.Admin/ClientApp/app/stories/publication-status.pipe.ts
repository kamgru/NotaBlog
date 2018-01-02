import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'publicationStatus'})
export class PublicationStatusPipe implements PipeTransform {

    transform(value: number) : string {
        switch (value) {
            case 0: return 'Draft';
            case 1: return 'Published';
            default: return '';
        }
    }
    
}