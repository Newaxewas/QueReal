import { RouterStateSnapshot, ActivatedRouteSnapshot, ResolveFn } from '@angular/router';
import { QuestGetRequest, QuestGetResponse } from '../common/api/models/quest';
import { inject } from '@angular/core';
import { QuestService } from '../common/api/services';

export const questResolver: ResolveFn<QuestGetResponse> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const questService = inject(QuestService);

  const request = new QuestGetRequest();
  request.id = route.paramMap.get("questId")!;

  return questService.get(request)
}
