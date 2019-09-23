C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config

使用非sqlserver的数据库，需要修改Machine.config

如：mysql，在Machine.config添加如下配置

    <system.data>
        <DbProviderFactories>
            <remove invariant="MySql.Data.MySqlClient" />
            <add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient" description=".Net Framework Data Provider for MySQL" type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=6.7.9.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />
        </DbProviderFactories>
    </system.data>