#include <iostream>
#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include <map>

#pragma comment(lib, "cpprest_2_10")

using namespace web;
using namespace web::http;
using namespace web::http::experimental::listener;
using namespace json;
using namespace std;

void handle_get(http_request request);
void handle_post(http_request request);

int main()
{
	http_listener listener(L"http://localhost:8777/Shape");

	listener.support(methods::GET, handle_get);
	listener.support(methods::POST, handle_post);

	try
	{
		listener.open().then([&listener]()
			{
				cout << "Server is listening... it can hear you!" << endl;
				wcout << listener.uri().to_string().c_str() << endl;
			}
		).wait();
	}
	catch (exception const& e)
	{
		cout << "Error starting server. \n\tReason: " << e.what() << endl;
	}

	while (true);

	return 0;
}

void handle_get(http_request request)
{
	cout << "Received GET request" << endl;


	request.reply(status_codes::OK, "Request received");
}

void handle_post(http_request request)
{
	cout << "Received POST request" << endl;


	request.reply(status_codes::OK, "Request received");
}