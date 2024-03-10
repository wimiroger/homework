namespace webdd.Models
{
    public class MenuValue
    {
        public List<Menu> Children { get; set; } = new List<Menu>()
            {
                new Menu()
                {
                    MName = "關於我",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "簡介",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "北區特色",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "南區特色",
                            Action = "About",
                            Controller = "Home"
                        }
                    }
                },
                new Menu()
                {
                    MName = "各單位介紹",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "行政中心",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "資訊中心",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "業務單位",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "產品研發",
                            Action = "About",
                            Controller = "Home"
                        }
                    }
                },
                new Menu()
                {
                    MName = "產品項目",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "周邊",
                            Children = new List<Menu>()
                            {
                                new Menu()
                                {
                                    MName = "滑鼠",
                                    Action = "About",
                                    Controller = "Home"
                                },
                                new Menu()
                                {
                                    MName = "鍵盤",
                                    Action = "About",
                                    Controller = "Home"
                                }
                            }
                        },
                        new Menu()
                        {
                            MName = "電池",
                            Action = "ProductPower",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "BIKE",
                            Action = "ProductBikeQuery",
                            Controller = "Product"
                        }
                    }
                },
                new Menu()
                    {
                        MName = "徵才",
                        Action = "About",
                        Controller = "Home"
                    },
                    new Menu()
                    {
                        MName = "新聞活動",
                        Action = "About",
                        Controller = "Home"
                    }
                };
    }
}
