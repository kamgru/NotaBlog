import { Injectable } from '@angular/core';
import { IStoryHeader } from './models/IStoryHeader';
import { IPaginatedData } from  '../models/IPaginatedData';
import { Observable } from 'rxjs/Observable';
import { IStory } from './models/IStory';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IApiResult } from '../models/IApiResult';

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
        return this.http.patch(`/api/stories/${id}/content`, {title: title, content: content});
    }

    public updateStatus(id:string, status:number): Observable<any> {
        return this.http.patch(`/api/stories/${id}/publication-status`, {storyStatus: status});
    }
}