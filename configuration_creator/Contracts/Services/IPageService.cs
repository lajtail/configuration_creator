﻿using System.Windows.Controls;

namespace configuration_creator.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
