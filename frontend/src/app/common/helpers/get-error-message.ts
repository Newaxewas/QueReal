import { HttpErrorResponse } from "@angular/common/http";

export function getErrorMessage(errorResponse: HttpErrorResponse): string {
  if (errorResponse.status === 0) {
    return "Connection error";
  } else {
    return errorResponse.error;
  }
}
