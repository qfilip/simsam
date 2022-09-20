//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '@Workspace/services';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IBaseDto } from './interfaces';

@Injectable() export class BaseApiController
{
	public Parameterless() : Observable<IBaseDto>
	{
		const body = <any>{  };
		return this.httpClient.get<IBaseDto>(
		this.settingsService.createApiUrl('BaseApi/Parameterless'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true,
			params: new HttpParams({ fromObject: body })
		})
		.pipe(map(response => response.body));
		
	}
	public Parametrized(id: any) : Observable<IBaseDto>
	{
		const body = <any>{ 'id': id };
		return this.httpClient.get<IBaseDto>(
		this.settingsService.createApiUrl('BaseApi/Parametrized'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true,
			params: new HttpParams({ fromObject: body })
		})
		.pipe(map(response => response.body));
		
	}
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
