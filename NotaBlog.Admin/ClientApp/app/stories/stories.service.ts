import { Injectable } from '@angular/core';
import { IStoryHeader } from './models/IStoryHeader';
import { IPaginatedData } from  '../models/IPaginatedData';
import { Observable } from 'rxjs/Observable';
import { IStory } from './models/IStory';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class StoriesService {

    constructor(private http: HttpClient) {}

    public getStoryHeaders(page:number, count:number): Observable<IPaginatedData<IStoryHeader>> {

        const params = new HttpParams()
            .set('page', page.toString())
            .set('count', count.toString());

        return this.http.get<IPaginatedData<IStoryHeader>>('/api/stories', {params});
    }

    public updateStory(id:string, title:string, content:string): Observable<any> {
        return this.http.patch(`/api/stories/${id}`, {title: title, content: content});
    }
}