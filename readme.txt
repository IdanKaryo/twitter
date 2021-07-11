Twitter

Please follow the steps to run the applications:

1. Provide any working MySql connection string in ".\twitter-net-core\Twitter\appsettings.json"
2. Run the Twitter API (IIS Express)
3. Navigate to ".\twitter-angular\"
4. Run npm install
5. Run npm start

Basic and short flow example - ".\twitter_basic_flow.mp4"

Implementd features:

1. signup/signin/logout - authentication management with Bearer token
2. messages(global feed) - authorized, state with ngrx, signalR is used for push notifications, virtual rendering.
3. filter messages by username or partial
3. error handling

Missing:

1. follow and personal feed
2. performance improvements (cache, paging etc)
3. tests (unit/integration/component/e2e)
4. dockerization
5. design document



