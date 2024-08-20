import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { fakeAsync, TestBed, tick } from '@angular/core/testing';
import { ErrorsComponent } from './errors.component';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { of } from 'rxjs';

describe('ErrorsComponent', () => {
  let component: ErrorsComponent;
  let httpTestingController: HttpTestingController;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);

    TestBed.configureTestingModule({
      imports: [ErrorsComponent],
      providers: [
        provideHttpClientTesting(),
        { provide: HttpClient, useValue: httpClientSpy },
        ErrorsComponent,
      ],
    });

    httpTestingController = TestBed.inject(HttpTestingController);
    component = TestBed.inject(ErrorsComponent);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should handle 400 error', fakeAsync(() => {
    component.get400Error();

    tick();
    const req = httpTestingController.expectOne(
      'https://localhost:5001/api/error/bad-request'
    );
    expect(req.request.method).toEqual('GET');
    req.flush(null, { status: 400, statusText: 'Bad Request' });

    tick();
  }));

  it('should handle 401 error', () => {
    component.get401Error();

    const req = httpTestingController.expectOne(
      'https://localhost:5001/api/error/auth'
    );
    expect(req.request.method).toEqual('GET');
    req.flush(null, { status: 401, statusText: 'Unauthorized' });

    // Add any expectations for the component after handling the error
  });

  it('should handle 404 error', fakeAsync(() => {
    component.get404Error();

    tick();

    const req = httpTestingController.expectOne(
      'https://localhost:5001/api/error/not-found'
    );
    expect(req.request.method).toEqual('GET');
    req.flush(null, { status: 404, statusText: 'Not Found' });

    tick();
  }));

  it('should handle 500 error', fakeAsync(() => {
    const errorResponse = new ErrorEvent('API error', {
      error: new Error('500 Server Error'),
      message: '500 Server Error',
    });

    httpClientSpy.get.and.returnValue(of(errorResponse));

    component.get500Error();

    tick();

    expect(httpClientSpy.get).toHaveBeenCalledWith(
      'https://localhost:5001/api/error/server-error'
    );
  }));

  it('should handle 400 validation error', fakeAsync(() => {
    const mockErrorResponse = { status: 400, statusText: 'Bad Request' };
    const data = 'Invalid request parameters';

    tick();

    component.get400ValidationError();

    const req = httpTestingController.expectOne(
      'https://localhost:5001/api/account/register'
    );
    expect(req.request.method).toEqual('POST');
    req.flush(data, mockErrorResponse);

    expect(component.validationErrors).toEqual(data);

    tick();
  }));
});
