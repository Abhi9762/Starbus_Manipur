<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ControlRoom.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="controlRoomDash.aspx.cs" Inherits="Auth_controlRoomDash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="refresh" content="180" />
    <style>
        ul {
            margin: 0;
            padding: 0;
            list-style-type: none;
            width: 100%;
            margin: 0 auto;
            text-align: center;
            overflow-x: hidden;
        }

        .cardd {
            float: left;
            position: relative;
            width: calc(100% * .3333 - 30px + .3333 * 30px);
            height: 100px;
            margin: 0 30px 30px 0;
            perspective: 1000;
            color: white;
        }

            .cardd:first-child .card__front {
                background: #db6623;
            }

            .cardd:first-child .card__num {
                text-shadow: 1px 1px rgb(153 71 24 / 80%), 2px 2px rgb(154 71 24 / 80%), 3px 3px rgb(155 72 24 / 80%), 4px 4px rgb(156 72 24 / 81%), 5px 5px rgb(157 73 25 / 81%), 6px 6px rgb(158 73 25 / 81%), 7px 7px rgb(159 74 25 / 81%), 8px 8px rgb(160 74 25 / 81%), 9px 9px rgb(161 75 25 / 82%), 10px 10px rgb(162 75 25 / 82%), 11px 11px rgb(162 75 26 / 82%), 12px 12px rgb(163 76 26 / 82%), 13px 13px rgb(164 76 26 / 82%), 14px 14px rgb(165 77 26 / 83%), 15px 15px rgb(166 77 26 / 83%), 16px 16px rgb(167 77 26 / 83%), 17px 17px rgb(168 78 26 / 83%), 18px 18px rgb(169 78 27 / 83%), 19px 19px rgb(169 79 27 / 84%), 20px 20px rgb(170 79 27 / 84%), 21px 21px rgb(171 79 27 / 84%), 22px 22px rgb(172 80 27 / 84%), 23px 23px rgb(173 80 27 / 84%), 24px 24px rgb(173 81 27 / 85%), 25px 25px rgb(174 81 28 / 85%), 26px 26px rgb(175 81 28 / 85%), 27px 27px rgb(176 82 28 / 85%), 28px 28px rgb(177 82 28 / 85%), 29px 29px rgb(177 82 28 / 86%), 30px 30px rgb(178 83 28 / 86%), 31px 31px rgb(179 83 28 / 86%), 32px 32px rgb(180 83 28 / 86%), 33px 33px rgb(180 84 29 / 86%), 34px 34px rgb(181 84 29 / 87%), 35px 35px rgb(182 85 29 / 87%), 36px 36px rgb(182 85 29 / 87%), 37px 37px rgb(183 85 29 / 87%), 38px 38px rgb(184 86 29 / 87%), 39px 39px rgb(185 86 29 / 88%), 40px 40px rgb(185 86 29 / 88%), 41px 41px rgb(186 87 30 / 88%), 42px 42px rgb(187 87 30 / 88%), 43px 43px rgb(187 87 30 / 88%), 44px 44px rgb(188 87 30 / 89%), 45px 45px rgb(189 88 30 / 89%), 46px 46px rgb(189 88 30 / 89%), 47px 47px rgb(190 88 30 / 89%), 48px 48px rgb(191 89 30 / 89%), 49px 49px rgb(191 89 30 / 90%), 50px 50px rgb(192 89 30 / 90%), 51px 51px rgb(193 90 31 / 90%), 52px 52px rgb(193 90 31 / 90%), 53px 53px rgb(194 90 31 / 90%), 54px 54px rgb(194 90 31 / 91%), 55px 55px rgb(195 91 31 / 91%), 56px 56px rgb(196 91 31 / 91%), 57px 57px rgb(196 91 31 / 91%), 58px 58px rgb(197 92 31 / 91%), 59px 59px rgb(198 92 31 / 92%), 60px 60px rgb(198 92 32 / 92%), 61px 61px rgb(199 92 32 / 92%), 62px 62px rgb(199 93 32 / 92%), 63px 63px rgb(200 93 32 / 92%), 64px 64px rgb(200 93 32 / 93%), 65px 65px rgb(201 94 32 / 93%), 66px 66px rgb(202 94 32 / 93%), 67px 67px rgb(202 94 32 / 93%), 68px 68px rgb(203 94 32 / 93%), 69px 69px rgb(203 95 32 / 94%), 70px 70px rgb(204 95 32 / 94%), 71px 71px rgb(204 95 33 / 94%), 72px 72px rgb(205 95 33 / 94%), 73px 73px rgb(205 96 33 / 94%), 74px 74px rgb(206 96 33 / 95%), 75px 75px rgb(206 96 33 / 95%), 76px 76px rgb(207 96 33 / 95%), 77px 77px rgb(208 97 33 / 95%), 78px 78px rgb(208 97 33 / 95%), 79px 79px rgb(209 97 33 / 96%), 80px 80px rgb(209 97 33 / 96%), 81px 81px rgb(210 98 33 / 96%), 82px 82px rgb(210 98 34 / 96%), 83px 83px rgb(211 98 34 / 96%), 84px 84px rgb(211 98 34 / 97%), 85px 85px rgb(212 99 34 / 97%), 86px 86px rgb(212 99 34 / 97%), 87px 87px rgb(213 99 34 / 97%), 88px 88px rgb(213 99 34 / 97%), 89px 89px rgb(214 99 34 / 98%), 90px 90px rgb(214 100 34 / 98%), 91px 91px rgb(214 100 34 / 98%), 92px 92px rgb(215 100 34 / 98%), 93px 93px rgb(215 100 34 / 98%), 94px 94px rgb(216 101 34 / 99%), 95px 95px rgb(216 101 35 / 99%), 96px 96px rgb(217 101 35 / 99%), 97px 97px rgb(217 101 35 / 99%), 98px 98px rgb(218 101 35 / 99%), 99px 99px rgb(218 102 35 / 100%), 100px 100px rgb(219 102 35 / 100%);
            }

            .cardd:nth-child(2) .card__front {
                background: #3e5eb3;
            }

            .cardd:nth-child(2) .card__num {
                text-shadow: 1px 1px rgb(42 64 122 / 80%), 2px 2px rgb(42 64 123 / 80%), 3px 3px rgb(43 65 124 / 80%), 4px 4px rgb(43 65 125 / 81%), 5px 5px rgb(43 66 125 / 81%), 6px 6px rgb(43 66 126 / 81%), 7px 7px rgb(44 67 127 / 81%), 8px 8px rgb(44 67 128 / 81%), 9px 9px rgb(44 67 129 / 82%), 10px 10px rgb(45 68 129 / 82%), 11px 11px rgb(45 68 130 / 82%), 12px 12px rgb(45 69 131 / 82%), 13px 13px rgb(45 69 132 / 82%), 14px 14px rgb(46 69 132 / 83%), 15px 15px rgb(46 70 133 / 83%), 16px 16px rgb(46 70 134 / 83%), 17px 17px rgb(46 71 135 / 83%), 18px 18px rgb(47 71 135 / 83%), 19px 19px rgb(47 71 136 / 84%), 20px 20px rgb(47 72 137 / 84%), 21px 21px rgb(47 72 138 / 84%), 22px 22px rgb(48 73 138 / 84%), 23px 23px rgb(48 73 139 / 84%), 24px 24px rgb(48 73 140 / 85%), 25px 25px rgb(48 74 140 / 85%), 26px 26px rgb(49 74 141 / 85%), 27px 27px rgb(49 74 142 / 85%), 28px 28px rgb(49 75 142 / 85%), 29px 29px rgb(49 75 143 / 86%), 30px 30px rgb(50 75 144 / 86%), 31px 31px rgb(50 76 144 / 86%), 32px 32px rgb(50 76 145 / 86%), 33px 33px rgb(50 76 146 / 86%), 34px 34px rgb(50 77 146 / 87%), 35px 35px rgb(51 77 147 / 87%), 36px 36px rgb(51 77 147 / 87%), 37px 37px rgb(51 78 148 / 87%), 38px 38px rgb(51 78 149 / 87%), 39px 39px rgb(52 78 149 / 88%), 40px 40px rgb(52 79 150 / 88%), 41px 41px rgb(52 79 151 / 88%), 42px 42px rgb(52 79 151 / 88%), 43px 43px rgb(52 80 152 / 88%), 44px 44px rgb(53 80 152 / 89%), 45px 45px rgb(53 80 153 / 89%), 46px 46px rgb(53 81 153 / 89%), 47px 47px rgb(53 81 154 / 89%), 48px 48px rgb(53 81 155 / 89%), 49px 49px rgb(54 81 155 / 90%), 50px 50px rgb(54 82 156 / 90%), 51px 51px rgb(54 82 156 / 90%), 52px 52px rgb(54 82 157 / 90%), 53px 53px rgb(54 83 157 / 90%), 54px 54px rgb(55 83 158 / 91%), 55px 55px rgb(55 83 158 / 91%), 56px 56px rgb(55 83 159 / 91%), 57px 57px rgb(55 84 159 / 91%), 58px 58px rgb(55 84 160 / 91%), 59px 59px rgb(55 84 160 / 92%), 60px 60px rgb(56 85 161 / 92%), 61px 61px rgb(56 85 161 / 92%), 62px 62px rgb(56 85 162 / 92%), 63px 63px rgb(56 85 162 / 92%), 64px 64px rgb(56 86 163 / 93%), 65px 65px rgb(57 86 163 / 93%), 66px 66px rgb(57 86 164 / 93%), 67px 67px rgb(57 86 164 / 93%), 68px 68px rgb(57 87 165 / 93%), 69px 69px rgb(57 87 165 / 94%), 70px 70px rgb(57 87 166 / 94%), 71px 71px rgb(58 87 166 / 94%), 72px 72px rgb(58 88 167 / 94%), 73px 73px rgb(58 88 167 / 94%), 74px 74px rgb(58 88 168 / 95%), 75px 75px rgb(58 88 168 / 95%), 76px 76px rgb(58 89 169 / 95%), 77px 77px rgb(59 89 169 / 95%), 78px 78px rgb(59 89 170 / 95%), 79px 79px rgb(59 89 170 / 96%), 80px 80px rgb(59 89 170 / 96%), 81px 81px rgb(59 90 171 / 96%), 82px 82px rgb(59 90 171 / 96%), 83px 83px rgb(59 90 172 / 96%), 84px 84px rgb(60 90 172 / 97%), 85px 85px rgb(60 91 173 / 97%), 86px 86px rgb(60 91 173 / 97%), 87px 87px rgb(60 91 173 / 97%), 88px 88px rgb(60 91 174 / 97%), 89px 89px rgb(60 92 174 / 98%), 90px 90px rgb(60 92 175 / 98%), 91px 91px rgb(61 92 175 / 98%), 92px 92px rgb(61 92 175 / 98%), 93px 93px rgb(61 92 176 / 98%), 94px 94px rgb(61 93 176 / 99%), 95px 95px rgb(61 93 177 / 99%), 96px 96px rgb(61 93 177 / 99%), 97px 97px rgb(61 93 177 / 99%), 98px 98px rgb(62 93 178 / 99%), 99px 99px rgb(62 94 178 / 100%), 100px 100px rgb(62 94 179 / 100%);
            }

            .cardd:nth-child(3) {
                margin-right: 0;
            }

                .cardd:nth-child(3) .card__front {
                    background: #bdb235;
                }

                .cardd:nth-child(3) .card__num {
                    text-shadow: 1px 1px rgb(129 122 36 / 80%), 2px 2px rgb(130 123 36 / 80%), 3px 3px rgb(131 124 37 / 80%), 4px 4px rgb(132 124 37 / 81%), 5px 5px rgb(133 125 37 / 81%), 6px 6px rgb(133 126 37 / 81%), 7px 7px rgb(134 127 37 / 81%), 8px 8px rgb(135 128 38 / 81%), 9px 9px rgb(136 128 38 / 82%), 10px 10px rgb(137 129 38 / 82%), 11px 11px rgb(138 130 38 / 82%), 12px 12px rgb(138 131 39 / 82%), 13px 13px rgb(139 132 39 / 82%), 14px 14px rgb(140 132 39 / 83%), 15px 15px rgb(141 133 39 / 83%), 16px 16px rgb(142 134 40 / 83%), 17px 17px rgb(142 134 40 / 83%), 18px 18px rgb(143 135 40 / 83%), 19px 19px rgb(144 136 40 / 84%), 20px 20px rgb(145 137 40 / 84%), 21px 21px rgb(145 137 41 / 84%), 22px 22px rgb(146 138 41 / 84%), 23px 23px rgb(147 139 41 / 84%), 24px 24px rgb(148 139 41 / 85%), 25px 25px rgb(148 140 41 / 85%), 26px 26px rgb(149 141 42 / 85%), 27px 27px rgb(150 141 42 / 85%), 28px 28px rgb(150 142 42 / 85%), 29px 29px rgb(151 143 42 / 86%), 30px 30px rgb(152 143 42 / 86%), 31px 31px rgb(152 144 43 / 86%), 32px 32px rgb(153 145 43 / 86%), 33px 33px rgb(154 145 43 / 86%), 34px 34px rgb(154 146 43 / 87%), 35px 35px rgb(155 146 43 / 87%), 36px 36px rgb(156 147 44 / 87%), 37px 37px rgb(156 148 44 / 87%), 38px 38px rgb(157 148 44 / 87%), 39px 39px rgb(158 149 44 / 88%), 40px 40px rgb(158 149 44 / 88%), 41px 41px rgb(159 150 45 / 88%), 42px 42px rgb(160 151 45 / 88%), 43px 43px rgb(160 151 45 / 88%), 44px 44px rgb(161 152 45 / 89%), 45px 45px rgb(161 152 45 / 89%), 46px 46px rgb(162 153 45 / 89%), 47px 47px rgb(163 153 46 / 89%), 48px 48px rgb(163 154 46 / 89%), 49px 49px rgb(164 155 46 / 90%), 50px 50px rgb(164 155 46 / 90%), 51px 51px rgb(165 156 46 / 90%), 52px 52px rgb(166 156 46 / 90%), 53px 53px rgb(166 157 47 / 90%), 54px 54px rgb(167 157 47 / 91%), 55px 55px rgb(167 158 47 / 91%), 56px 56px rgb(168 158 47 / 91%), 57px 57px rgb(168 159 47 / 91%), 58px 58px rgb(169 159 47 / 91%), 59px 59px rgb(169 160 47 / 92%), 60px 60px rgb(170 160 48 / 92%), 61px 61px rgb(171 161 48 / 92%), 62px 62px rgb(171 161 48 / 92%), 63px 63px rgb(172 162 48 / 92%), 64px 64px rgb(172 162 48 / 93%), 65px 65px rgb(173 163 48 / 93%), 66px 66px rgb(173 163 49 / 93%), 67px 67px rgb(174 164 49 / 93%), 68px 68px rgb(174 164 49 / 93%), 69px 69px rgb(175 165 49 / 94%), 70px 70px rgb(175 165 49 / 94%), 71px 71px rgb(176 166 49 / 94%), 72px 72px rgb(176 166 49 / 94%), 73px 73px rgb(177 166 50 / 94%), 74px 74px rgb(177 167 50 / 95%), 75px 75px rgb(178 167 50 / 95%), 76px 76px rgb(178 168 50 / 95%), 77px 77px rgb(179 168 50 / 95%), 78px 78px rgb(179 169 50 / 95%), 79px 79px rgb(180 169 50 / 96%), 80px 80px rgb(180 170 50 / 96%), 81px 81px rgb(180 170 51 / 96%), 82px 82px rgb(181 170 51 / 96%), 83px 83px rgb(181 171 51 / 96%), 84px 84px rgb(182 171 51 / 97%), 85px 85px rgb(182 172 51 / 97%), 86px 86px rgb(183 172 51 / 97%), 87px 87px rgb(183 173 51 / 97%), 88px 88px rgb(184 173 51 / 97%), 89px 89px rgb(184 173 52 / 98%), 90px 90px rgb(184 174 52 / 98%), 91px 91px rgb(185 174 52 / 98%), 92px 92px rgb(185 175 52 / 98%), 93px 93px rgb(186 175 52 / 98%), 94px 94px rgb(186 175 52 / 99%), 95px 95px rgb(187 176 52 / 99%), 96px 96px rgb(187 176 52 / 99%), 97px 97px rgb(187 176 53 / 99%), 98px 98px rgb(188 177 53 / 99%), 99px 99px rgb(188 177 53 / 100%), 100px 100px rgb(189 178 53 / 100%);
                }

            .cardd:nth-child(4) .card__front {
                background: #5271c2;
            }

            .cardd:nth-child(4) .card__num {
                text-shadow: 1px 1px rgb(52 78 147 / 80%), 2px 2px rgb(52 79 148 / 80%), 3px 3px rgb(53 79 148 / 80%), 4px 4px rgb(53 80 149 / 81%), 5px 5px rgb(54 80 150 / 81%), 6px 6px rgb(54 81 150 / 81%), 7px 7px rgb(55 81 151 / 81%), 8px 8px rgb(55 82 152 / 81%), 9px 9px rgb(55 82 152 / 82%), 10px 10px rgb(56 83 153 / 82%), 11px 11px rgb(56 83 154 / 82%), 12px 12px rgb(57 83 154 / 82%), 13px 13px rgb(57 84 155 / 82%), 14px 14px rgb(57 84 156 / 83%), 15px 15px rgb(58 85 156 / 83%), 16px 16px rgb(58 85 157 / 83%), 17px 17px rgb(59 86 157 / 83%), 18px 18px rgb(59 86 158 / 83%), 19px 19px rgb(59 87 159 / 84%), 20px 20px rgb(60 87 159 / 84%), 21px 21px rgb(60 88 160 / 84%), 22px 22px rgb(61 88 160 / 84%), 23px 23px rgb(61 88 161 / 84%), 24px 24px rgb(61 89 162 / 85%), 25px 25px rgb(62 89 162 / 85%), 26px 26px rgb(62 90 163 / 85%), 27px 27px rgb(62 90 163 / 85%), 28px 28px rgb(63 90 164 / 85%), 29px 29px rgb(63 91 164 / 86%), 30px 30px rgb(63 91 165 / 86%), 31px 31px rgb(64 92 165 / 86%), 32px 32px rgb(64 92 166 / 86%), 33px 33px rgb(64 92 166 / 86%), 34px 34px rgb(65 93 167 / 87%), 35px 35px rgb(65 93 167 / 87%), 36px 36px rgb(65 94 168 / 87%), 37px 37px rgb(66 94 169 / 87%), 38px 38px rgb(66 94 169 / 87%), 39px 39px rgb(66 95 170 / 88%), 40px 40px rgb(67 95 170 / 88%), 41px 41px rgb(67 96 171 / 88%), 42px 42px rgb(67 96 171 / 88%), 43px 43px rgb(68 96 171 / 88%), 44px 44px rgb(68 97 172 / 89%), 45px 45px rgb(68 97 172 / 89%), 46px 46px rgb(69 97 173 / 89%), 47px 47px rgb(69 98 173 / 89%), 48px 48px rgb(69 98 174 / 89%), 49px 49px rgb(69 98 174 / 90%), 50px 50px rgb(70 99 175 / 90%), 51px 51px rgb(70 99 175 / 90%), 52px 52px rgb(70 99 176 / 90%), 53px 53px rgb(71 100 176 / 90%), 54px 54px rgb(71 100 177 / 91%), 55px 55px rgb(71 100 177 / 91%), 56px 56px rgb(71 101 177 / 91%), 57px 57px rgb(72 101 178 / 91%), 58px 58px rgb(72 101 178 / 91%), 59px 59px rgb(72 102 179 / 92%), 60px 60px rgb(73 102 179 / 92%), 61px 61px rgb(73 102 180 / 92%), 62px 62px rgb(73 103 180 / 92%), 63px 63px rgb(73 103 180 / 92%), 64px 64px rgb(74 103 181 / 93%), 65px 65px rgb(74 103 181 / 93%), 66px 66px rgb(74 104 182 / 93%), 67px 67px rgb(74 104 182 / 93%), 68px 68px rgb(75 104 182 / 93%), 69px 69px rgb(75 105 183 / 94%), 70px 70px rgb(75 105 183 / 94%), 71px 71px rgb(75 105 184 / 94%), 72px 72px rgb(76 106 184 / 94%), 73px 73px rgb(76 106 184 / 94%), 74px 74px rgb(76 106 185 / 95%), 75px 75px rgb(76 106 185 / 95%), 76px 76px rgb(77 107 185 / 95%), 77px 77px rgb(77 107 186 / 95%), 78px 78px rgb(77 107 186 / 95%), 79px 79px rgb(77 107 187 / 96%), 80px 80px rgb(77 108 187 / 96%), 81px 81px rgb(78 108 187 / 96%), 82px 82px rgb(78 108 188 / 96%), 83px 83px rgb(78 109 188 / 96%), 84px 84px rgb(78 109 188 / 97%), 85px 85px rgb(79 109 189 / 97%), 86px 86px rgb(79 109 189 / 97%), 87px 87px rgb(79 110 189 / 97%), 88px 88px rgb(79 110 190 / 97%), 89px 89px rgb(80 110 190 / 98%), 90px 90px rgb(80 110 190 / 98%), 91px 91px rgb(80 111 191 / 98%), 92px 92px rgb(80 111 191 / 98%), 93px 93px rgb(80 111 191 / 98%), 94px 94px rgb(81 111 192 / 99%), 95px 95px rgb(81 112 192 / 99%), 96px 96px rgb(81 112 192 / 99%), 97px 97px rgb(81 112 193 / 99%), 98px 98px rgb(81 112 193 / 99%), 99px 99px rgb(82 113 193), 100px 100px rgb(82 113 194);
            }

            .cardd:nth-child(5) .card__front {
                background: #35a541;
            }

            .cardd:nth-child(5) .card__num {
                text-shadow: 1px 1px rgb(34 107 42 / 80%), 2px 2px rgb(34 108 42 / 80%), 3px 3px rgb(35 109 43 / 80%), 4px 4px rgb(35 110 43 / 81%), 5px 5px rgb(35 110 43 / 81%), 6px 6px rgb(35 111 44 / 81%), 7px 7px rgb(36 112 44 / 81%), 8px 8px rgb(36 113 44 / 81%), 9px 9px rgb(36 114 45 / 82%), 10px 10px rgb(36 114 45 / 82%), 11px 11px rgb(37 115 45 / 82%), 12px 12px rgb(37 116 46 / 82%), 13px 13px rgb(37 117 46 / 82%), 14px 14px rgb(37 118 46 / 83%), 15px 15px rgb(38 118 47 / 83%), 16px 16px rgb(38 119 47 / 83%), 17px 17px rgb(38 120 47 / 83%), 18px 18px rgb(38 121 47 / 83%), 19px 19px rgb(39 121 48 / 84%), 20px 20px rgb(39 122 48 / 84%), 21px 21px rgb(39 123 48 / 84%), 22px 22px rgb(39 124 49 / 84%), 23px 23px rgb(40 124 49 / 84%), 24px 24px rgb(40 125 49 / 85%), 25px 25px rgb(40 126 49 / 85%), 26px 26px rgb(40 126 50 / 85%), 27px 27px rgb(41 127 50 / 85%), 28px 28px rgb(41 128 50 / 85%), 29px 29px rgb(41 128 50 / 86%), 30px 30px rgb(41 129 51 / 86%), 31px 31px rgb(41 130 51 / 86%), 32px 32px rgb(42 130 51 / 86%), 33px 33px rgb(42 131 52 / 86%), 34px 34px rgb(42 132 52 / 87%), 35px 35px rgb(42 132 52 / 87%), 36px 36px rgb(42 133 52 / 87%), 37px 37px rgb(43 134 53 / 87%), 38px 38px rgb(43 134 53 / 87%), 39px 39px rgb(43 135 53 / 88%), 40px 40px rgb(43 135 53 / 88%), 41px 41px rgb(44 136 54 / 88%), 42px 42px rgb(44 137 54 / 88%), 43px 43px rgb(44 137 54 / 88%), 44px 44px rgb(44 138 54 / 89%), 45px 45px rgb(44 138 54 / 89%), 46px 46px rgb(44 139 55 / 89%), 47px 47px rgb(45 140 55 / 89%), 48px 48px rgb(45 140 55 / 89%), 49px 49px rgb(45 141 55 / 90%), 50px 50px rgb(45 141 56 / 90%), 51px 51px rgb(45 142 56 / 90%), 52px 52px rgb(46 142 56 / 90%), 53px 53px rgb(46 143 56 / 90%), 54px 54px rgb(46 143 56 / 91%), 55px 55px rgb(46 144 57 / 91%), 56px 56px rgb(46 145 57 / 91%), 57px 57px rgb(46 145 57 / 91%), 58px 58px rgb(47 146 57 / 91%), 59px 59px rgb(47 146 58 / 92%), 60px 60px rgb(47 147 58 / 92%), 61px 61px rgb(47 147 58 / 92%), 62px 62px rgb(47 148 58 / 92%), 63px 63px rgb(47 148 58 / 92%), 64px 64px rgb(48 149 59 / 93%), 65px 65px rgb(48 149 59 / 93%), 66px 66px rgb(48 150 59 / 93%), 67px 67px rgb(48 150 59 / 93%), 68px 68px rgb(48 151 59 / 93%), 69px 69px rgb(48 151 60 / 94%), 70px 70px rgb(49 152 60 / 94%), 71px 71px rgb(49 152 60 / 94%), 72px 72px rgb(49 153 60 / 94%), 73px 73px rgb(49 153 60 / 94%), 74px 74px rgb(49 154 60 / 95%), 75px 75px rgb(49 154 61 / 95%), 76px 76px rgb(50 154 61 / 95%), 77px 77px rgb(50 155 61 / 95%), 78px 78px rgb(50 155 61 / 95%), 79px 79px rgb(50 156 61 / 96%), 80px 80px rgb(50 156 62 / 96%), 81px 81px rgb(50 157 62 / 96%), 82px 82px rgb(50 157 62 / 96%), 83px 83px rgb(51 158 62 / 96%), 84px 84px rgb(51 158 62 / 97%), 85px 85px rgb(51 158 62 / 97%), 86px 86px rgb(51 159 63 / 97%), 87px 87px rgb(51 159 63 / 97%), 88px 88px rgb(51 160 63 / 97%), 89px 89px rgb(51 160 63 / 98%), 90px 90px rgb(52 161 63 / 98%), 91px 91px rgb(52 161 63 / 98%), 92px 92px rgb(52 161 64 / 98%), 93px 93px rgb(52 162 64 / 98%), 94px 94px rgb(52 162 64 / 99%), 95px 95px rgb(52 163 64 / 99%), 96px 96px rgb(52 163 64 / 99%), 97px 97px rgb(52 163 64 / 99%), 98px 98px rgb(53 164 65 / 99%), 99px 99px rgb(53 164 65), 100px 100px rgb(53 165 65);
            }

            .cardd:nth-child(6) .card__front {
                background: #aa9e5c;
            }

            .cardd:nth-child(6) .card__num {
                text-shadow: 1px 1px rgb(122 113 64 / 80%), 2px 2px rgb(123 114 64 / 80%), 3px 3px rgb(123 114 65 / 80%), 4px 4px rgb(124 115 65 / 81%), 5px 5px rgb(125 116 66 / 81%), 6px 6px rgb(126 116 66 / 81%), 7px 7px rgb(126 117 66 / 81%), 8px 8px rgb(127 118 67 / 81%), 9px 9px rgb(128 118 67 / 82%), 10px 10px rgb(128 119 68 / 82%), 11px 11px rgb(129 119 68 / 82%), 12px 12px rgb(130 120 68 / 82%), 13px 13px rgb(130 121 69 / 82%), 14px 14px rgb(131 121 69 / 83%), 15px 15px rgb(131 122 69 / 83%), 16px 16px rgb(132 122 70 / 83%), 17px 17px rgb(133 123 70 / 83%), 18px 18px rgb(133 124 71 / 83%), 19px 19px rgb(134 124 71 / 84%), 20px 20px rgb(134 125 71 / 84%), 21px 21px rgb(135 125 72 / 84%), 22px 22px rgb(136 126 72 / 84%), 23px 23px rgb(136 126 72 / 84%), 24px 24px rgb(137 127 73 / 85%), 25px 25px rgb(137 127 73 / 85%), 26px 26px rgb(138 128 73 / 85%), 27px 27px rgb(139 129 74 / 85%), 28px 28px rgb(139 129 74 / 85%), 29px 29px rgb(140 130 74 / 86%), 30px 30px rgb(140 130 75 / 86%), 31px 31px rgb(141 131 75 / 86%), 32px 32px rgb(141 131 75 / 86%), 33px 33px rgb(142 132 76 / 86%), 34px 34px rgb(142 132 76 / 87%), 35px 35px rgb(143 133 76 / 87%), 36px 36px rgb(143 133 77 / 87%), 37px 37px rgb(144 134 77 / 87%), 38px 38px rgb(144 134 77 / 87%), 39px 39px rgb(145 135 77 / 88%), 40px 40px rgb(145 135 78 / 88%), 41px 41px rgb(146 136 78 / 88%), 42px 42px rgb(146 136 78 / 88%), 43px 43px rgb(147 136 79 / 88%), 44px 44px rgb(147 137 79 / 89%), 45px 45px rgb(148 137 79 / 89%), 46px 46px rgb(148 138 79 / 89%), 47px 47px rgb(149 138 80 / 89%), 48px 48px rgb(149 139 80 / 89%), 49px 49px rgb(150 139 80 / 90%), 50px 50px rgb(150 140 81 / 90%), 51px 51px rgb(151 140 81 / 90%), 52px 52px rgb(151 140 81 / 90%), 53px 53px rgb(152 141 81 / 90%), 54px 54px rgb(152 141 82 / 91%), 55px 55px rgb(153 142 82 / 91%), 56px 56px rgb(153 142 82 / 91%), 57px 57px rgb(154 143 82 / 91%), 58px 58px rgb(154 143 83 / 91%), 59px 59px rgb(154 143 83 / 92%), 60px 60px rgb(155 144 83 / 92%), 61px 61px rgb(155 144 83 / 92%), 62px 62px rgb(156 145 84 / 92%), 63px 63px rgb(156 145 84 / 92%), 64px 64px rgb(156 145 84 / 93%), 65px 65px rgb(157 146 84 / 93%), 66px 66px rgb(157 146 85 / 93%), 67px 67px rgb(158 146 85 / 93%), 68px 68px rgb(158 147 85 / 93%), 69px 69px rgb(159 147 85 / 94%), 70px 70px rgb(159 148 86 / 94%), 71px 71px rgb(159 148 86 / 94%), 72px 72px rgb(160 148 86 / 94%), 73px 73px rgb(160 149 86 / 94%), 74px 74px rgb(161 149 86 / 95%), 75px 75px rgb(161 149 87 / 95%), 76px 76px rgb(161 150 87 / 95%), 77px 77px rgb(162 150 87 / 95%), 78px 78px rgb(162 151 87 / 95%), 79px 79px rgb(162 151 88 / 96%), 80px 80px rgb(163 151 88 / 96%), 81px 81px rgb(163 152 88 / 96%), 82px 82px rgb(164 152 88 / 96%), 83px 83px rgb(164 152 88 / 96%), 84px 84px rgb(164 153 89 / 97%), 85px 85px rgb(165 153 89 / 97%), 86px 86px rgb(165 153 89 / 97%), 87px 87px rgb(165 154 89 / 97%), 88px 88px rgb(166 154 89 / 97%), 89px 89px rgb(166 154 90 / 98%), 90px 90px rgb(166 155 90 / 98%), 91px 91px rgb(167 155 90 / 98%), 92px 92px rgb(167 155 90 / 98%), 93px 93px rgb(167 156 90 / 98%), 94px 94px rgb(168 156 91 / 99%), 95px 95px rgb(168 156 91 / 99%), 96px 96px rgb(168 156 91 / 99%), 97px 97px rgb(169 157 91 / 99%), 98px 98px rgb(169 157 91 / 99%), 99px 99px rgb(169 157 92 / 100%), 100px 100px rgb(170 158 92 / 100%);
            }


            .cardd:last-child {
                margin-right: 0;
            }

        .card__flipper {
            transform-style: preserve-3d;
            transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
        }

        .card__front, .card__back {
            position: absolute;
            backface-visibility: hidden;
            top: 0;
            left: 0;
            width: 100%;
            height: 100px;
        }

        .card__front {
            transform: rotateY(0);
            z-index: 2;
            overflow: hidden;
            border-radius: 10px;
        }

        .card__back {
            transform: rotateY(180deg) scale(1.1);
            background: #141414;
            display: flex;
            flex-flow: column wrap;
            align-items: center;
            justify-content: center;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
        }

            .card__back span {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
            }

        .card__name {
            font-size: 18px;
            line-height: 0.9;
            font-weight: 700;
        }

            .card__name span {
                font-size: 14px;
            }

        .card__num {
            font-size: 65px;
            margin: 0 8px 0 0;
            font-weight: 700;
        }

        @media (max-width: 700px) {
            .card__num {
                font-size: 70px;
            }
        }

        @media (max-width: 700px) {
            .cardd {
                width: 100%;
                height: 100px;
                margin-right: 0;
                float: none;
            }

                .cardd .card__front, .cardd .card__back {
                    height: 100px;
                }
        }
    </style>
    <style>
        ul {
            margin: 0;
            padding: 0;
            list-style-type: none;
            width: 100%;
            margin: 0 auto;
            text-align: center;
            overflow-x: hidden;
        }

        .carddd {
            float: left;
            position: relative;
            width: calc(100% * .3333 - 30px + .3333 * 30px);
            height: 100px;
            margin: 0 30px 30px 0;
            perspective: 1000;
            color: white;
        }

            .carddd:first-child .card__front {
                background: #5271c2;
            }

            .carddd:first-child .card__num {
                text-shadow: 1px 1px rgb(52 78 147 / 80%), 2px 2px rgb(52 79 148 / 80%), 3px 3px rgb(53 79 148 / 80%), 4px 4px rgb(53 80 149 / 81%), 5px 5px rgb(54 80 150 / 81%), 6px 6px rgb(54 81 150 / 81%), 7px 7px rgb(55 81 151 / 81%), 8px 8px rgb(55 82 152 / 81%), 9px 9px rgb(55 82 152 / 82%), 10px 10px rgb(56 83 153 / 82%), 11px 11px rgb(56 83 154 / 82%), 12px 12px rgb(57 83 154 / 82%), 13px 13px rgb(57 84 155 / 82%), 14px 14px rgb(57 84 156 / 83%), 15px 15px rgb(58 85 156 / 83%), 16px 16px rgb(58 85 157 / 83%), 17px 17px rgb(59 86 157 / 83%), 18px 18px rgb(59 86 158 / 83%), 19px 19px rgb(59 87 159 / 84%), 20px 20px rgb(60 87 159 / 84%), 21px 21px rgb(60 88 160 / 84%), 22px 22px rgb(61 88 160 / 84%), 23px 23px rgb(61 88 161 / 84%), 24px 24px rgb(61 89 162 / 85%), 25px 25px rgb(62 89 162 / 85%), 26px 26px rgb(62 90 163 / 85%), 27px 27px rgb(62 90 163 / 85%), 28px 28px rgb(63 90 164 / 85%), 29px 29px rgb(63 91 164 / 86%), 30px 30px rgb(63 91 165 / 86%), 31px 31px rgb(64 92 165 / 86%), 32px 32px rgb(64 92 166 / 86%), 33px 33px rgb(64 92 166 / 86%), 34px 34px rgb(65 93 167 / 87%), 35px 35px rgb(65 93 167 / 87%), 36px 36px rgb(65 94 168 / 87%), 37px 37px rgb(66 94 169 / 87%), 38px 38px rgb(66 94 169 / 87%), 39px 39px rgb(66 95 170 / 88%), 40px 40px rgb(67 95 170 / 88%), 41px 41px rgb(67 96 171 / 88%), 42px 42px rgb(67 96 171 / 88%), 43px 43px rgb(68 96 171 / 88%), 44px 44px rgb(68 97 172 / 89%), 45px 45px rgb(68 97 172 / 89%), 46px 46px rgb(69 97 173 / 89%), 47px 47px rgb(69 98 173 / 89%), 48px 48px rgb(69 98 174 / 89%), 49px 49px rgb(69 98 174 / 90%), 50px 50px rgb(70 99 175 / 90%), 51px 51px rgb(70 99 175 / 90%), 52px 52px rgb(70 99 176 / 90%), 53px 53px rgb(71 100 176 / 90%), 54px 54px rgb(71 100 177 / 91%), 55px 55px rgb(71 100 177 / 91%), 56px 56px rgb(71 101 177 / 91%), 57px 57px rgb(72 101 178 / 91%), 58px 58px rgb(72 101 178 / 91%), 59px 59px rgb(72 102 179 / 92%), 60px 60px rgb(73 102 179 / 92%), 61px 61px rgb(73 102 180 / 92%), 62px 62px rgb(73 103 180 / 92%), 63px 63px rgb(73 103 180 / 92%), 64px 64px rgb(74 103 181 / 93%), 65px 65px rgb(74 103 181 / 93%), 66px 66px rgb(74 104 182 / 93%), 67px 67px rgb(74 104 182 / 93%), 68px 68px rgb(75 104 182 / 93%), 69px 69px rgb(75 105 183 / 94%), 70px 70px rgb(75 105 183 / 94%), 71px 71px rgb(75 105 184 / 94%), 72px 72px rgb(76 106 184 / 94%), 73px 73px rgb(76 106 184 / 94%), 74px 74px rgb(76 106 185 / 95%), 75px 75px rgb(76 106 185 / 95%), 76px 76px rgb(77 107 185 / 95%), 77px 77px rgb(77 107 186 / 95%), 78px 78px rgb(77 107 186 / 95%), 79px 79px rgb(77 107 187 / 96%), 80px 80px rgb(77 108 187 / 96%), 81px 81px rgb(78 108 187 / 96%), 82px 82px rgb(78 108 188 / 96%), 83px 83px rgb(78 109 188 / 96%), 84px 84px rgb(78 109 188 / 97%), 85px 85px rgb(79 109 189 / 97%), 86px 86px rgb(79 109 189 / 97%), 87px 87px rgb(79 110 189 / 97%), 88px 88px rgb(79 110 190 / 97%), 89px 89px rgb(80 110 190 / 98%), 90px 90px rgb(80 110 190 / 98%), 91px 91px rgb(80 111 191 / 98%), 92px 92px rgb(80 111 191 / 98%), 93px 93px rgb(80 111 191 / 98%), 94px 94px rgb(81 111 192 / 99%), 95px 95px rgb(81 112 192 / 99%), 96px 96px rgb(81 112 192 / 99%), 97px 97px rgb(81 112 193 / 99%), 98px 98px rgb(81 112 193 / 99%), 99px 99px rgb(82 113 193), 100px 100px rgb(82 113 194);
            }

            .carddd:nth-child(2) .card__front {
                background: #35a541;
            }

            .carddd:nth-child(2) .card__num {
                text-shadow: 1px 1px rgb(34 107 42 / 80%), 2px 2px rgb(34 108 42 / 80%), 3px 3px rgb(35 109 43 / 80%), 4px 4px rgb(35 110 43 / 81%), 5px 5px rgb(35 110 43 / 81%), 6px 6px rgb(35 111 44 / 81%), 7px 7px rgb(36 112 44 / 81%), 8px 8px rgb(36 113 44 / 81%), 9px 9px rgb(36 114 45 / 82%), 10px 10px rgb(36 114 45 / 82%), 11px 11px rgb(37 115 45 / 82%), 12px 12px rgb(37 116 46 / 82%), 13px 13px rgb(37 117 46 / 82%), 14px 14px rgb(37 118 46 / 83%), 15px 15px rgb(38 118 47 / 83%), 16px 16px rgb(38 119 47 / 83%), 17px 17px rgb(38 120 47 / 83%), 18px 18px rgb(38 121 47 / 83%), 19px 19px rgb(39 121 48 / 84%), 20px 20px rgb(39 122 48 / 84%), 21px 21px rgb(39 123 48 / 84%), 22px 22px rgb(39 124 49 / 84%), 23px 23px rgb(40 124 49 / 84%), 24px 24px rgb(40 125 49 / 85%), 25px 25px rgb(40 126 49 / 85%), 26px 26px rgb(40 126 50 / 85%), 27px 27px rgb(41 127 50 / 85%), 28px 28px rgb(41 128 50 / 85%), 29px 29px rgb(41 128 50 / 86%), 30px 30px rgb(41 129 51 / 86%), 31px 31px rgb(41 130 51 / 86%), 32px 32px rgb(42 130 51 / 86%), 33px 33px rgb(42 131 52 / 86%), 34px 34px rgb(42 132 52 / 87%), 35px 35px rgb(42 132 52 / 87%), 36px 36px rgb(42 133 52 / 87%), 37px 37px rgb(43 134 53 / 87%), 38px 38px rgb(43 134 53 / 87%), 39px 39px rgb(43 135 53 / 88%), 40px 40px rgb(43 135 53 / 88%), 41px 41px rgb(44 136 54 / 88%), 42px 42px rgb(44 137 54 / 88%), 43px 43px rgb(44 137 54 / 88%), 44px 44px rgb(44 138 54 / 89%), 45px 45px rgb(44 138 54 / 89%), 46px 46px rgb(44 139 55 / 89%), 47px 47px rgb(45 140 55 / 89%), 48px 48px rgb(45 140 55 / 89%), 49px 49px rgb(45 141 55 / 90%), 50px 50px rgb(45 141 56 / 90%), 51px 51px rgb(45 142 56 / 90%), 52px 52px rgb(46 142 56 / 90%), 53px 53px rgb(46 143 56 / 90%), 54px 54px rgb(46 143 56 / 91%), 55px 55px rgb(46 144 57 / 91%), 56px 56px rgb(46 145 57 / 91%), 57px 57px rgb(46 145 57 / 91%), 58px 58px rgb(47 146 57 / 91%), 59px 59px rgb(47 146 58 / 92%), 60px 60px rgb(47 147 58 / 92%), 61px 61px rgb(47 147 58 / 92%), 62px 62px rgb(47 148 58 / 92%), 63px 63px rgb(47 148 58 / 92%), 64px 64px rgb(48 149 59 / 93%), 65px 65px rgb(48 149 59 / 93%), 66px 66px rgb(48 150 59 / 93%), 67px 67px rgb(48 150 59 / 93%), 68px 68px rgb(48 151 59 / 93%), 69px 69px rgb(48 151 60 / 94%), 70px 70px rgb(49 152 60 / 94%), 71px 71px rgb(49 152 60 / 94%), 72px 72px rgb(49 153 60 / 94%), 73px 73px rgb(49 153 60 / 94%), 74px 74px rgb(49 154 60 / 95%), 75px 75px rgb(49 154 61 / 95%), 76px 76px rgb(50 154 61 / 95%), 77px 77px rgb(50 155 61 / 95%), 78px 78px rgb(50 155 61 / 95%), 79px 79px rgb(50 156 61 / 96%), 80px 80px rgb(50 156 62 / 96%), 81px 81px rgb(50 157 62 / 96%), 82px 82px rgb(50 157 62 / 96%), 83px 83px rgb(51 158 62 / 96%), 84px 84px rgb(51 158 62 / 97%), 85px 85px rgb(51 158 62 / 97%), 86px 86px rgb(51 159 63 / 97%), 87px 87px rgb(51 159 63 / 97%), 88px 88px rgb(51 160 63 / 97%), 89px 89px rgb(51 160 63 / 98%), 90px 90px rgb(52 161 63 / 98%), 91px 91px rgb(52 161 63 / 98%), 92px 92px rgb(52 161 64 / 98%), 93px 93px rgb(52 162 64 / 98%), 94px 94px rgb(52 162 64 / 99%), 95px 95px rgb(52 163 64 / 99%), 96px 96px rgb(52 163 64 / 99%), 97px 97px rgb(52 163 64 / 99%), 98px 98px rgb(53 164 65 / 99%), 99px 99px rgb(53 164 65), 100px 100px rgb(53 165 65);
            }

            .carddd:nth-child(3) {
                margin-right: 0;
            }

                .carddd:nth-child(3) .card__front {
                    background: #aa9e5c;
                }

                .carddd:nth-child(3) .card__num {
                    text-shadow: 1px 1px rgb(122 113 64 / 80%), 2px 2px rgb(123 114 64 / 80%), 3px 3px rgb(123 114 65 / 80%), 4px 4px rgb(124 115 65 / 81%), 5px 5px rgb(125 116 66 / 81%), 6px 6px rgb(126 116 66 / 81%), 7px 7px rgb(126 117 66 / 81%), 8px 8px rgb(127 118 67 / 81%), 9px 9px rgb(128 118 67 / 82%), 10px 10px rgb(128 119 68 / 82%), 11px 11px rgb(129 119 68 / 82%), 12px 12px rgb(130 120 68 / 82%), 13px 13px rgb(130 121 69 / 82%), 14px 14px rgb(131 121 69 / 83%), 15px 15px rgb(131 122 69 / 83%), 16px 16px rgb(132 122 70 / 83%), 17px 17px rgb(133 123 70 / 83%), 18px 18px rgb(133 124 71 / 83%), 19px 19px rgb(134 124 71 / 84%), 20px 20px rgb(134 125 71 / 84%), 21px 21px rgb(135 125 72 / 84%), 22px 22px rgb(136 126 72 / 84%), 23px 23px rgb(136 126 72 / 84%), 24px 24px rgb(137 127 73 / 85%), 25px 25px rgb(137 127 73 / 85%), 26px 26px rgb(138 128 73 / 85%), 27px 27px rgb(139 129 74 / 85%), 28px 28px rgb(139 129 74 / 85%), 29px 29px rgb(140 130 74 / 86%), 30px 30px rgb(140 130 75 / 86%), 31px 31px rgb(141 131 75 / 86%), 32px 32px rgb(141 131 75 / 86%), 33px 33px rgb(142 132 76 / 86%), 34px 34px rgb(142 132 76 / 87%), 35px 35px rgb(143 133 76 / 87%), 36px 36px rgb(143 133 77 / 87%), 37px 37px rgb(144 134 77 / 87%), 38px 38px rgb(144 134 77 / 87%), 39px 39px rgb(145 135 77 / 88%), 40px 40px rgb(145 135 78 / 88%), 41px 41px rgb(146 136 78 / 88%), 42px 42px rgb(146 136 78 / 88%), 43px 43px rgb(147 136 79 / 88%), 44px 44px rgb(147 137 79 / 89%), 45px 45px rgb(148 137 79 / 89%), 46px 46px rgb(148 138 79 / 89%), 47px 47px rgb(149 138 80 / 89%), 48px 48px rgb(149 139 80 / 89%), 49px 49px rgb(150 139 80 / 90%), 50px 50px rgb(150 140 81 / 90%), 51px 51px rgb(151 140 81 / 90%), 52px 52px rgb(151 140 81 / 90%), 53px 53px rgb(152 141 81 / 90%), 54px 54px rgb(152 141 82 / 91%), 55px 55px rgb(153 142 82 / 91%), 56px 56px rgb(153 142 82 / 91%), 57px 57px rgb(154 143 82 / 91%), 58px 58px rgb(154 143 83 / 91%), 59px 59px rgb(154 143 83 / 92%), 60px 60px rgb(155 144 83 / 92%), 61px 61px rgb(155 144 83 / 92%), 62px 62px rgb(156 145 84 / 92%), 63px 63px rgb(156 145 84 / 92%), 64px 64px rgb(156 145 84 / 93%), 65px 65px rgb(157 146 84 / 93%), 66px 66px rgb(157 146 85 / 93%), 67px 67px rgb(158 146 85 / 93%), 68px 68px rgb(158 147 85 / 93%), 69px 69px rgb(159 147 85 / 94%), 70px 70px rgb(159 148 86 / 94%), 71px 71px rgb(159 148 86 / 94%), 72px 72px rgb(160 148 86 / 94%), 73px 73px rgb(160 149 86 / 94%), 74px 74px rgb(161 149 86 / 95%), 75px 75px rgb(161 149 87 / 95%), 76px 76px rgb(161 150 87 / 95%), 77px 77px rgb(162 150 87 / 95%), 78px 78px rgb(162 151 87 / 95%), 79px 79px rgb(162 151 88 / 96%), 80px 80px rgb(163 151 88 / 96%), 81px 81px rgb(163 152 88 / 96%), 82px 82px rgb(164 152 88 / 96%), 83px 83px rgb(164 152 88 / 96%), 84px 84px rgb(164 153 89 / 97%), 85px 85px rgb(165 153 89 / 97%), 86px 86px rgb(165 153 89 / 97%), 87px 87px rgb(165 154 89 / 97%), 88px 88px rgb(166 154 89 / 97%), 89px 89px rgb(166 154 90 / 98%), 90px 90px rgb(166 155 90 / 98%), 91px 91px rgb(167 155 90 / 98%), 92px 92px rgb(167 155 90 / 98%), 93px 93px rgb(167 156 90 / 98%), 94px 94px rgb(168 156 91 / 99%), 95px 95px rgb(168 156 91 / 99%), 96px 96px rgb(168 156 91 / 99%), 97px 97px rgb(169 157 91 / 99%), 98px 98px rgb(169 157 91 / 99%), 99px 99px rgb(169 157 92 / 100%), 100px 100px rgb(170 158 92 / 100%);
                }

            .carddd:nth-child(4) .card__front {
                background: #5271c2;
            }

            .carddd:nth-child(4) .card__num {
                text-shadow: 1px 1px rgb(52 78 147 / 80%), 2px 2px rgb(52 79 148 / 80%), 3px 3px rgb(53 79 148 / 80%), 4px 4px rgb(53 80 149 / 81%), 5px 5px rgb(54 80 150 / 81%), 6px 6px rgb(54 81 150 / 81%), 7px 7px rgb(55 81 151 / 81%), 8px 8px rgb(55 82 152 / 81%), 9px 9px rgb(55 82 152 / 82%), 10px 10px rgb(56 83 153 / 82%), 11px 11px rgb(56 83 154 / 82%), 12px 12px rgb(57 83 154 / 82%), 13px 13px rgb(57 84 155 / 82%), 14px 14px rgb(57 84 156 / 83%), 15px 15px rgb(58 85 156 / 83%), 16px 16px rgb(58 85 157 / 83%), 17px 17px rgb(59 86 157 / 83%), 18px 18px rgb(59 86 158 / 83%), 19px 19px rgb(59 87 159 / 84%), 20px 20px rgb(60 87 159 / 84%), 21px 21px rgb(60 88 160 / 84%), 22px 22px rgb(61 88 160 / 84%), 23px 23px rgb(61 88 161 / 84%), 24px 24px rgb(61 89 162 / 85%), 25px 25px rgb(62 89 162 / 85%), 26px 26px rgb(62 90 163 / 85%), 27px 27px rgb(62 90 163 / 85%), 28px 28px rgb(63 90 164 / 85%), 29px 29px rgb(63 91 164 / 86%), 30px 30px rgb(63 91 165 / 86%), 31px 31px rgb(64 92 165 / 86%), 32px 32px rgb(64 92 166 / 86%), 33px 33px rgb(64 92 166 / 86%), 34px 34px rgb(65 93 167 / 87%), 35px 35px rgb(65 93 167 / 87%), 36px 36px rgb(65 94 168 / 87%), 37px 37px rgb(66 94 169 / 87%), 38px 38px rgb(66 94 169 / 87%), 39px 39px rgb(66 95 170 / 88%), 40px 40px rgb(67 95 170 / 88%), 41px 41px rgb(67 96 171 / 88%), 42px 42px rgb(67 96 171 / 88%), 43px 43px rgb(68 96 171 / 88%), 44px 44px rgb(68 97 172 / 89%), 45px 45px rgb(68 97 172 / 89%), 46px 46px rgb(69 97 173 / 89%), 47px 47px rgb(69 98 173 / 89%), 48px 48px rgb(69 98 174 / 89%), 49px 49px rgb(69 98 174 / 90%), 50px 50px rgb(70 99 175 / 90%), 51px 51px rgb(70 99 175 / 90%), 52px 52px rgb(70 99 176 / 90%), 53px 53px rgb(71 100 176 / 90%), 54px 54px rgb(71 100 177 / 91%), 55px 55px rgb(71 100 177 / 91%), 56px 56px rgb(71 101 177 / 91%), 57px 57px rgb(72 101 178 / 91%), 58px 58px rgb(72 101 178 / 91%), 59px 59px rgb(72 102 179 / 92%), 60px 60px rgb(73 102 179 / 92%), 61px 61px rgb(73 102 180 / 92%), 62px 62px rgb(73 103 180 / 92%), 63px 63px rgb(73 103 180 / 92%), 64px 64px rgb(74 103 181 / 93%), 65px 65px rgb(74 103 181 / 93%), 66px 66px rgb(74 104 182 / 93%), 67px 67px rgb(74 104 182 / 93%), 68px 68px rgb(75 104 182 / 93%), 69px 69px rgb(75 105 183 / 94%), 70px 70px rgb(75 105 183 / 94%), 71px 71px rgb(75 105 184 / 94%), 72px 72px rgb(76 106 184 / 94%), 73px 73px rgb(76 106 184 / 94%), 74px 74px rgb(76 106 185 / 95%), 75px 75px rgb(76 106 185 / 95%), 76px 76px rgb(77 107 185 / 95%), 77px 77px rgb(77 107 186 / 95%), 78px 78px rgb(77 107 186 / 95%), 79px 79px rgb(77 107 187 / 96%), 80px 80px rgb(77 108 187 / 96%), 81px 81px rgb(78 108 187 / 96%), 82px 82px rgb(78 108 188 / 96%), 83px 83px rgb(78 109 188 / 96%), 84px 84px rgb(78 109 188 / 97%), 85px 85px rgb(79 109 189 / 97%), 86px 86px rgb(79 109 189 / 97%), 87px 87px rgb(79 110 189 / 97%), 88px 88px rgb(79 110 190 / 97%), 89px 89px rgb(80 110 190 / 98%), 90px 90px rgb(80 110 190 / 98%), 91px 91px rgb(80 111 191 / 98%), 92px 92px rgb(80 111 191 / 98%), 93px 93px rgb(80 111 191 / 98%), 94px 94px rgb(81 111 192 / 99%), 95px 95px rgb(81 112 192 / 99%), 96px 96px rgb(81 112 192 / 99%), 97px 97px rgb(81 112 193 / 99%), 98px 98px rgb(81 112 193 / 99%), 99px 99px rgb(82 113 193), 100px 100px rgb(82 113 194);
            }

            .carddd:nth-child(5) .card__front {
                background: #35a541;
            }

            .carddd:nth-child(5) .card__num {
                text-shadow: 1px 1px rgb(34 107 42 / 80%), 2px 2px rgb(34 108 42 / 80%), 3px 3px rgb(35 109 43 / 80%), 4px 4px rgb(35 110 43 / 81%), 5px 5px rgb(35 110 43 / 81%), 6px 6px rgb(35 111 44 / 81%), 7px 7px rgb(36 112 44 / 81%), 8px 8px rgb(36 113 44 / 81%), 9px 9px rgb(36 114 45 / 82%), 10px 10px rgb(36 114 45 / 82%), 11px 11px rgb(37 115 45 / 82%), 12px 12px rgb(37 116 46 / 82%), 13px 13px rgb(37 117 46 / 82%), 14px 14px rgb(37 118 46 / 83%), 15px 15px rgb(38 118 47 / 83%), 16px 16px rgb(38 119 47 / 83%), 17px 17px rgb(38 120 47 / 83%), 18px 18px rgb(38 121 47 / 83%), 19px 19px rgb(39 121 48 / 84%), 20px 20px rgb(39 122 48 / 84%), 21px 21px rgb(39 123 48 / 84%), 22px 22px rgb(39 124 49 / 84%), 23px 23px rgb(40 124 49 / 84%), 24px 24px rgb(40 125 49 / 85%), 25px 25px rgb(40 126 49 / 85%), 26px 26px rgb(40 126 50 / 85%), 27px 27px rgb(41 127 50 / 85%), 28px 28px rgb(41 128 50 / 85%), 29px 29px rgb(41 128 50 / 86%), 30px 30px rgb(41 129 51 / 86%), 31px 31px rgb(41 130 51 / 86%), 32px 32px rgb(42 130 51 / 86%), 33px 33px rgb(42 131 52 / 86%), 34px 34px rgb(42 132 52 / 87%), 35px 35px rgb(42 132 52 / 87%), 36px 36px rgb(42 133 52 / 87%), 37px 37px rgb(43 134 53 / 87%), 38px 38px rgb(43 134 53 / 87%), 39px 39px rgb(43 135 53 / 88%), 40px 40px rgb(43 135 53 / 88%), 41px 41px rgb(44 136 54 / 88%), 42px 42px rgb(44 137 54 / 88%), 43px 43px rgb(44 137 54 / 88%), 44px 44px rgb(44 138 54 / 89%), 45px 45px rgb(44 138 54 / 89%), 46px 46px rgb(44 139 55 / 89%), 47px 47px rgb(45 140 55 / 89%), 48px 48px rgb(45 140 55 / 89%), 49px 49px rgb(45 141 55 / 90%), 50px 50px rgb(45 141 56 / 90%), 51px 51px rgb(45 142 56 / 90%), 52px 52px rgb(46 142 56 / 90%), 53px 53px rgb(46 143 56 / 90%), 54px 54px rgb(46 143 56 / 91%), 55px 55px rgb(46 144 57 / 91%), 56px 56px rgb(46 145 57 / 91%), 57px 57px rgb(46 145 57 / 91%), 58px 58px rgb(47 146 57 / 91%), 59px 59px rgb(47 146 58 / 92%), 60px 60px rgb(47 147 58 / 92%), 61px 61px rgb(47 147 58 / 92%), 62px 62px rgb(47 148 58 / 92%), 63px 63px rgb(47 148 58 / 92%), 64px 64px rgb(48 149 59 / 93%), 65px 65px rgb(48 149 59 / 93%), 66px 66px rgb(48 150 59 / 93%), 67px 67px rgb(48 150 59 / 93%), 68px 68px rgb(48 151 59 / 93%), 69px 69px rgb(48 151 60 / 94%), 70px 70px rgb(49 152 60 / 94%), 71px 71px rgb(49 152 60 / 94%), 72px 72px rgb(49 153 60 / 94%), 73px 73px rgb(49 153 60 / 94%), 74px 74px rgb(49 154 60 / 95%), 75px 75px rgb(49 154 61 / 95%), 76px 76px rgb(50 154 61 / 95%), 77px 77px rgb(50 155 61 / 95%), 78px 78px rgb(50 155 61 / 95%), 79px 79px rgb(50 156 61 / 96%), 80px 80px rgb(50 156 62 / 96%), 81px 81px rgb(50 157 62 / 96%), 82px 82px rgb(50 157 62 / 96%), 83px 83px rgb(51 158 62 / 96%), 84px 84px rgb(51 158 62 / 97%), 85px 85px rgb(51 158 62 / 97%), 86px 86px rgb(51 159 63 / 97%), 87px 87px rgb(51 159 63 / 97%), 88px 88px rgb(51 160 63 / 97%), 89px 89px rgb(51 160 63 / 98%), 90px 90px rgb(52 161 63 / 98%), 91px 91px rgb(52 161 63 / 98%), 92px 92px rgb(52 161 64 / 98%), 93px 93px rgb(52 162 64 / 98%), 94px 94px rgb(52 162 64 / 99%), 95px 95px rgb(52 163 64 / 99%), 96px 96px rgb(52 163 64 / 99%), 97px 97px rgb(52 163 64 / 99%), 98px 98px rgb(53 164 65 / 99%), 99px 99px rgb(53 164 65), 100px 100px rgb(53 165 65);
            }

            .carddd:nth-child(6) .card__front {
                background: #aa9e5c;
            }

            .carddd:nth-child(6) .card__num {
                text-shadow: 1px 1px rgb(122 113 64 / 80%), 2px 2px rgb(123 114 64 / 80%), 3px 3px rgb(123 114 65 / 80%), 4px 4px rgb(124 115 65 / 81%), 5px 5px rgb(125 116 66 / 81%), 6px 6px rgb(126 116 66 / 81%), 7px 7px rgb(126 117 66 / 81%), 8px 8px rgb(127 118 67 / 81%), 9px 9px rgb(128 118 67 / 82%), 10px 10px rgb(128 119 68 / 82%), 11px 11px rgb(129 119 68 / 82%), 12px 12px rgb(130 120 68 / 82%), 13px 13px rgb(130 121 69 / 82%), 14px 14px rgb(131 121 69 / 83%), 15px 15px rgb(131 122 69 / 83%), 16px 16px rgb(132 122 70 / 83%), 17px 17px rgb(133 123 70 / 83%), 18px 18px rgb(133 124 71 / 83%), 19px 19px rgb(134 124 71 / 84%), 20px 20px rgb(134 125 71 / 84%), 21px 21px rgb(135 125 72 / 84%), 22px 22px rgb(136 126 72 / 84%), 23px 23px rgb(136 126 72 / 84%), 24px 24px rgb(137 127 73 / 85%), 25px 25px rgb(137 127 73 / 85%), 26px 26px rgb(138 128 73 / 85%), 27px 27px rgb(139 129 74 / 85%), 28px 28px rgb(139 129 74 / 85%), 29px 29px rgb(140 130 74 / 86%), 30px 30px rgb(140 130 75 / 86%), 31px 31px rgb(141 131 75 / 86%), 32px 32px rgb(141 131 75 / 86%), 33px 33px rgb(142 132 76 / 86%), 34px 34px rgb(142 132 76 / 87%), 35px 35px rgb(143 133 76 / 87%), 36px 36px rgb(143 133 77 / 87%), 37px 37px rgb(144 134 77 / 87%), 38px 38px rgb(144 134 77 / 87%), 39px 39px rgb(145 135 77 / 88%), 40px 40px rgb(145 135 78 / 88%), 41px 41px rgb(146 136 78 / 88%), 42px 42px rgb(146 136 78 / 88%), 43px 43px rgb(147 136 79 / 88%), 44px 44px rgb(147 137 79 / 89%), 45px 45px rgb(148 137 79 / 89%), 46px 46px rgb(148 138 79 / 89%), 47px 47px rgb(149 138 80 / 89%), 48px 48px rgb(149 139 80 / 89%), 49px 49px rgb(150 139 80 / 90%), 50px 50px rgb(150 140 81 / 90%), 51px 51px rgb(151 140 81 / 90%), 52px 52px rgb(151 140 81 / 90%), 53px 53px rgb(152 141 81 / 90%), 54px 54px rgb(152 141 82 / 91%), 55px 55px rgb(153 142 82 / 91%), 56px 56px rgb(153 142 82 / 91%), 57px 57px rgb(154 143 82 / 91%), 58px 58px rgb(154 143 83 / 91%), 59px 59px rgb(154 143 83 / 92%), 60px 60px rgb(155 144 83 / 92%), 61px 61px rgb(155 144 83 / 92%), 62px 62px rgb(156 145 84 / 92%), 63px 63px rgb(156 145 84 / 92%), 64px 64px rgb(156 145 84 / 93%), 65px 65px rgb(157 146 84 / 93%), 66px 66px rgb(157 146 85 / 93%), 67px 67px rgb(158 146 85 / 93%), 68px 68px rgb(158 147 85 / 93%), 69px 69px rgb(159 147 85 / 94%), 70px 70px rgb(159 148 86 / 94%), 71px 71px rgb(159 148 86 / 94%), 72px 72px rgb(160 148 86 / 94%), 73px 73px rgb(160 149 86 / 94%), 74px 74px rgb(161 149 86 / 95%), 75px 75px rgb(161 149 87 / 95%), 76px 76px rgb(161 150 87 / 95%), 77px 77px rgb(162 150 87 / 95%), 78px 78px rgb(162 151 87 / 95%), 79px 79px rgb(162 151 88 / 96%), 80px 80px rgb(163 151 88 / 96%), 81px 81px rgb(163 152 88 / 96%), 82px 82px rgb(164 152 88 / 96%), 83px 83px rgb(164 152 88 / 96%), 84px 84px rgb(164 153 89 / 97%), 85px 85px rgb(165 153 89 / 97%), 86px 86px rgb(165 153 89 / 97%), 87px 87px rgb(165 154 89 / 97%), 88px 88px rgb(166 154 89 / 97%), 89px 89px rgb(166 154 90 / 98%), 90px 90px rgb(166 155 90 / 98%), 91px 91px rgb(167 155 90 / 98%), 92px 92px rgb(167 155 90 / 98%), 93px 93px rgb(167 156 90 / 98%), 94px 94px rgb(168 156 91 / 99%), 95px 95px rgb(168 156 91 / 99%), 96px 96px rgb(168 156 91 / 99%), 97px 97px rgb(169 157 91 / 99%), 98px 98px rgb(169 157 91 / 99%), 99px 99px rgb(169 157 92 / 100%), 100px 100px rgb(170 158 92 / 100%);
            }


            .carddd:last-child {
                margin-right: 0;
            }

        .card__flipper {
            transform-style: preserve-3d;
            transition: all 0.6s cubic-bezier(0.23, 1, 0.32, 1);
        }

        .card__front, .card__back {
            position: absolute;
            backface-visibility: hidden;
            top: 0;
            left: 0;
            width: 100%;
            height: 100px;
        }

        .card__front {
            transform: rotateY(0);
            z-index: 2;
            overflow: hidden;
            border-radius: 4px;
        }

        .card__back {
            transform: rotateY(180deg) scale(1.1);
            background: #141414;
            display: flex;
            flex-flow: column wrap;
            align-items: center;
            justify-content: center;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
        }

            .card__back span {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
            }

        .card__name {
            font-size: 18px;
            line-height: 0.9;
            font-weight: 700;
        }

            .card__name span {
                font-size: 14px;
            }

        .card__num {
            font-size: 65px;
            line-height: 1;
            margin: 0 8px 0 0;
            font-weight: 700;
        }

        @media (max-width: 700px) {
            .card__num {
                font-size: 70px;
            }
        }

        @media (max-width: 700px) {
            .carddd {
                width: 100%;
                height: 100px;
                margin-right: 0;
                float: none;
            }

                .carddd .card__front, .carddd .card__back {
                    height: 100px;
                }
        }
    </style>
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
    <style>
        .custom-toggle {
            position: relative;
            display: inherit;
            display: inline-block;
            width: 52px;
            height: 1.5rem;
            margin: 0;
        }

            .custom-toggle input {
                display: none;
            }

                .custom-toggle input:checked + .custom-toggle-slider {
                    border: 1px solid #043e95;
                }

                    .custom-toggle input:checked + .custom-toggle-slider:before {
                        transform: translateX(28px);
                        background: #043e95;
                    }

                .custom-toggle input:disabled + .custom-toggle-slider {
                    border: 1px solid #e9ecef;
                }

                .custom-toggle input:disabled:checked + .custom-toggle-slider {
                    border: 1px solid #e9ecef;
                }

                    .custom-toggle input:disabled:checked + .custom-toggle-slider:before {
                        background-color: #8a98eb;
                    }

        .custom-toggle-slider {
            position: absolute;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            cursor: pointer;
            border: 1px solid #ced4da;
            border-radius: 34px !important;
            background-color: transparent;
        }

            .custom-toggle-slider:before {
                position: absolute;
                bottom: 2px;
                left: 2px;
                width: 18px;
                height: 18px;
                content: '';
                transition: all .15s cubic-bezier(.68, -.55, .265, 1.55);
                border-radius: 50% !important;
                background-color: #e9ecef;
            }

        .custom-toggle-wrapper .custom-toggle + .custom-toggle {
            margin-left: 1rem !important;
        }

        .custom-toggle input:checked + .custom-toggle-slider:after {
            right: auto;
            left: 0;
            content: attr(data-label-on);
            color: #043e95;
        }

        .custom-toggle-slider:after {
            font-family: inherit;
            font-size: .75rem;
            font-weight: 600;
            line-height: 24px;
            position: absolute;
            top: 0;
            right: 0;
            display: block;
            overflow: hidden;
            min-width: 1.66667rem;
            margin: 0 .21667rem;
            content: attr(data-label-off);
            transition: all .15s ease;
            text-align: center;
            color: #ced4da;
        }

        @media (prefers-reduced-motion: reduce) {
            .custom-toggle-slider:after {
                transition: none;
            }
        }

        .custom-toggle-primary input:checked + .custom-toggle-slider {
            border-color: #043e95;
        }

            .custom-toggle-primary input:checked + .custom-toggle-slider:before {
                background: #043e95;
            }

            .custom-toggle-primary input:checked + .custom-toggle-slider:after {
                color: #043e95;
            }

        .custom-toggle-primary input:disabled:checked + .custom-toggle-slider {
            border-color: #043e95;
        }

            .custom-toggle-primary input:disabled:checked + .custom-toggle-slider:before {
                background-color: #8a98eb;
            }

        .custom-toggle-secondary input:checked + .custom-toggle-slider {
            border-color: #f7fafc;
        }

            .custom-toggle-secondary input:checked + .custom-toggle-slider:before {
                background: #f7fafc;
            }

            .custom-toggle-secondary input:checked + .custom-toggle-slider:after {
                color: #f7fafc;
            }

        .custom-toggle-secondary input:disabled:checked + .custom-toggle-slider {
            border-color: #f7fafc;
        }

            .custom-toggle-secondary input:disabled:checked + .custom-toggle-slider:before {
                background-color: white;
            }

        .custom-toggle-success input:checked + .custom-toggle-slider {
            border-color: #2dce89;
        }

            .custom-toggle-success input:checked + .custom-toggle-slider:before {
                background: #2dce89;
            }

            .custom-toggle-success input:checked + .custom-toggle-slider:after {
                color: #2dce89;
            }

        .custom-toggle-success input:disabled:checked + .custom-toggle-slider {
            border-color: #2dce89;
        }

            .custom-toggle-success input:disabled:checked + .custom-toggle-slider:before {
                background-color: #54daa1;
            }

        .custom-toggle-info input:checked + .custom-toggle-slider {
            border-color: #11cdef;
        }

            .custom-toggle-info input:checked + .custom-toggle-slider:before {
                background: #11cdef;
            }

            .custom-toggle-info input:checked + .custom-toggle-slider:after {
                color: #11cdef;
            }

        .custom-toggle-info input:disabled:checked + .custom-toggle-slider {
            border-color: #11cdef;
        }

            .custom-toggle-info input:disabled:checked + .custom-toggle-slider:before {
                background-color: #41d7f2;
            }

        .custom-toggle-warning input:checked + .custom-toggle-slider {
            border-color: #fb6340;
        }

            .custom-toggle-warning input:checked + .custom-toggle-slider:before {
                background: #fb6340;
            }

            .custom-toggle-warning input:checked + .custom-toggle-slider:after {
                color: #fb6340;
            }

        .custom-toggle-warning input:disabled:checked + .custom-toggle-slider {
            border-color: #fb6340;
        }

            .custom-toggle-warning input:disabled:checked + .custom-toggle-slider:before {
                background-color: #fc8c72;
            }

        .custom-toggle-danger input:checked + .custom-toggle-slider {
            border-color: #f5365c;
        }

            .custom-toggle-danger input:checked + .custom-toggle-slider:before {
                background: #f5365c;
            }

            .custom-toggle-danger input:checked + .custom-toggle-slider:after {
                color: #f5365c;
            }

        .custom-toggle-danger input:disabled:checked + .custom-toggle-slider {
            border-color: #f5365c;
        }

            .custom-toggle-danger input:disabled:checked + .custom-toggle-slider:before {
                background-color: #f76783;
            }

        .custom-toggle-light input:checked + .custom-toggle-slider {
            border-color: #adb5bd;
        }

            .custom-toggle-light input:checked + .custom-toggle-slider:before {
                background: #adb5bd;
            }

            .custom-toggle-light input:checked + .custom-toggle-slider:after {
                color: #adb5bd;
            }

        .custom-toggle-light input:disabled:checked + .custom-toggle-slider {
            border-color: #adb5bd;
        }

            .custom-toggle-light input:disabled:checked + .custom-toggle-slider:before {
                background-color: #c9cfd4;
            }

        .custom-toggle-dark input:checked + .custom-toggle-slider {
            border-color: #212529;
        }

            .custom-toggle-dark input:checked + .custom-toggle-slider:before {
                background: #212529;
            }

            .custom-toggle-dark input:checked + .custom-toggle-slider:after {
                color: #212529;
            }

        .custom-toggle-dark input:disabled:checked + .custom-toggle-slider {
            border-color: #212529;
        }

            .custom-toggle-dark input:disabled:checked + .custom-toggle-slider:before {
                background-color: #383f45;
            }

        .custom-toggle-default input:checked + .custom-toggle-slider {
            border-color: #172b4d;
        }

            .custom-toggle-default input:checked + .custom-toggle-slider:before {
                background: #172b4d;
            }

            .custom-toggle-default input:checked + .custom-toggle-slider:after {
                color: #172b4d;
            }

        .custom-toggle-default input:disabled:checked + .custom-toggle-slider {
            border-color: #172b4d;
        }

            .custom-toggle-default input:disabled:checked + .custom-toggle-slider:before {
                background-color: #234174;
            }

        .custom-toggle-white input:checked + .custom-toggle-slider {
            border-color: #fff;
        }

            .custom-toggle-white input:checked + .custom-toggle-slider:before {
                background: #fff;
            }

            .custom-toggle-white input:checked + .custom-toggle-slider:after {
                color: #fff;
            }

        .custom-toggle-white input:disabled:checked + .custom-toggle-slider {
            border-color: #fff;
        }

            .custom-toggle-white input:disabled:checked + .custom-toggle-slider:before {
                background-color: white;
            }

        .custom-toggle-neutral input:checked + .custom-toggle-slider {
            border-color: #fff;
        }

            .custom-toggle-neutral input:checked + .custom-toggle-slider:before {
                background: #fff;
            }

            .custom-toggle-neutral input:checked + .custom-toggle-slider:after {
                color: #fff;
            }

        .custom-toggle-neutral input:disabled:checked + .custom-toggle-slider {
            border-color: #fff;
        }

            .custom-toggle-neutral input:disabled:checked + .custom-toggle-slider:before {
                background-color: white;
            }

        .custom-toggle-darker input:checked + .custom-toggle-slider {
            border-color: black;
        }

            .custom-toggle-darker input:checked + .custom-toggle-slider:before {
                background: black;
            }

            .custom-toggle-darker input:checked + .custom-toggle-slider:after {
                color: black;
            }

        .custom-toggle-darker input:disabled:checked + .custom-toggle-slider {
            border-color: black;
        }

            .custom-toggle-darker input:disabled:checked + .custom-toggle-slider:before {
                background-color: #1a1a1a;
            }
    </style>
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery-n.js"></script>
    <script src="../DataTables/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBusList').DataTable({
                "pageLength": 7,
                //dom: 'Bfrtip',
                "bSort": false,
                buttons: [
                    'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
        $(document).ready(function () {
            $('#gvEmpList').DataTable({
                "pageLength": 7,
                //dom: 'Bfrtip',
                "bSort": false,
                buttons: [
                    'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
    <style>
        .dataTables_length {
            display: none;
        }

        hr {
            display: block;
            height: 1px;
            border: 0;
            border-top: 2px solid #ccc;
            margin-top: -5px;
            padding: 0;
        }
    </style>

    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="background-color: #F1F1F1;">
        <div class="col-xl-5 col-lg-5 col-md-6 col-sm-12 mb-xl-0 pb-4">
            <h5 style="color:#074886">1. Traveller Grievance & Alerts <span class="m-lg-2" style="font-size: 14px;">as on <%= DateTime.Now.ToString("dd MMM yyyy hh:mm tt")  %>
            </span></h5>
            <hr />
            <h6 style="color:#074886">Latest Alerts/Grievance</h6>
            <div class="row px-3 mb-3" >
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <h5 class="mb-0" style="color:#074886">Alerts</h5>
                            <p class="mb-1" style="font-size: 12px !important">Within 0-10 minutes</p>
                            <h2 class="mb-0" >
                                <asp:LinkButton ID="lbtnAlertCount_10Min" style="color:#074886" OnClick="lbtnAlertCount_10Min_Click" runat="server" Text="123"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <h5 class="mb-0" style="color:#074886">Alerts</h5>
                            <p class="mb-1" style="font-size: 12px !important">Within 0-2 hours</p>
                            <h2 class="mb-0">
                                <asp:LinkButton ID="lbtnAlertCount_2Hours" style="color:#074886" runat="server" OnClick="lbtnAlertCount_2Hours_Click" Text="NA"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <h5 class="mb-0" style="color:#074886">Grievance</h5>
                            <p class="mb-1" style="font-size: 12px !important">Within 0-24 hours</p>
                            <h2 class="mb-0">
                                <asp:LinkButton ID="lbtnGrievanceCount_24Hours" style="color:#074886" runat="server" OnClick="lbtnGrievanceCount_24Hours_Click" Text="NA"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
            </div>
            <h6 style="color:#074886">Grievance Summary</h6>
            <div class="row px-3 mb-5">
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <p class="mb-0">Pending</p>
                            <h2 class="mb-0">
                                <asp:LinkButton style="color:#074886" ID="lbtnGrievanceCount_pending" runat="server" OnClick="lbtnGrievanceCount_pending_Click" Text="NA"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <p class="mb-0" >Assign</p>
                            <h2 class="mb-0">
                                <asp:LinkButton style="color:#074886" ID="lbtnGrievanceCount_assigned" runat="server" Text="NA" OnClick="lbtnGrievanceCount_assigned_Click"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="card">
                        <div class="card-body px-3 py-2">
                            <p class="mb-0" >Returned</p>
                            <h2 class="mb-0">
                                <asp:LinkButton style="color:#074886" ID="lbtnGrievanceCount_returned" runat="server" Text="NA" OnClick="lbtnGrievanceCount_returned_Click"></asp:LinkButton>
                            </h2>
                        </div>
                    </div>
                </div>
            </div>

            <h5 style="color:#074886">2. Fleet & Crew <span class="m-lg-2" style="font-size: 14px;">as on <%= DateTime.Now.ToString("dd MMM yyyy hh:mm tt")  %>
            </span></h5>
            <hr />
            <div class="row px-3 mb-3">
                <div class="col-sm-4">
                    <ol class="list-group list-group">
                        <li class="list-group-item font-weight-bold" aria-current="true" style="background: #074886; color: white;">Bus</li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Total</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:Label ID="lbtnTotalBus" runat="server" Text="0"></asp:Label>
                            </span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">On Duty</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnOndutyBus" runat="server" OnClick="lbtnOndutyBus_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Free</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnFreeBus" runat="server" OnClick="lbtnFreeBus_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-4">
                    <ol class="list-group list-group">
                        <li class="list-group-item font-weight-bold" aria-current="true" style="background: #074886; color: white;">Driver</li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Total</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:Label ID="lbtnTotalDriver" runat="server" Text="0"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">On Duty</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnOndutyDRiver" runat="server" OnClick="lbtnOndutyDRiver_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Free</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnFreeDriver" runat="server" OnClick="lbtnFreeDriver_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-4">
                    <ol class="list-group list-group">
                        <li class="list-group-item font-weight-bold" aria-current="true" style="background: #074886; color: white;">Conductor</li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Total</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:Label ID="lbtnTotalConductor" runat="server" Text="0"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">On Duty</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnOndutyConductor" runat="server" OnClick="lbtnOndutyConductor_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-start">
                            <p class="mb-0">Free</p>
                            <span class="badge rounded-pill" style="background: #074886; color: white;">
                                <asp:LinkButton ID="lbtnFreeConductor" runat="server" OnClick="lbtnFreeConductor_Click" Text="0" CssClass="text-white"></asp:LinkButton>
                            </span>
                        </li>
                    </ol>
                </div>
            </div>

        </div>
        <div class="col-xl-7 col-lg-7 col-md-6 col-sm-12 mb-xl-0 mb-4 mt-4">
            <div class="card shadow" style="min-height: 80vh;">
                <div class="card-header py-3 px-3" id="listHeader"  runat="server">
                    <div class="row">
                        <div class="col-6">
                            <h5>
                                <asp:Label ID="lblListHeader" style="color:#074886"  runat="server" Text="0-10 Minutes"></asp:Label></h5>

                        </div>

                        <div class="col-6" runat="server" id="divRadio" visible="false">

                            <label class="custom-toggle" style="float: right">
                                <asp:CheckBox ID="chkFreeDuty" AutoPostBack="true" OnCheckedChanged="cbConfirmTickets_CheckedChanged" runat="server" />
                                <span class="custom-toggle-slider rounded-circle" data-label-on="Duty" data-label-off="Free"></span>
                            </label>
                        </div>

                    </div>


                </div>

                <div class="card-body pt-0 p-3">
                    <ul class="list-group">
                        <asp:GridView ID="gvAlarms" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="w-100" OnRowDataBound="gvAlarms_RowDataBound" ShowHeader="false" OnRowCommand="gvAlarms_RowCommand" DataKeyNames="report_refno,busno">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <li class="list-group-item border-0 d-flex p-4 mb-2 bg-gray-100 border-radius-lg">
                                            <div class="d-flex flex-column">
                                                <h6 class="mb-3 text-sm" style="text-align: left; color:#074886"><%# Eval("alarm_type") %></h6>
                                                <span class="mb-2 text-xs" style="text-align: left;">PNR: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("ticket_no") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Reported at: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("report_datetime") %></span></span>
                                            </div>
                                            <div class="d-flex flex-column ps-5">
                                                <h6 class="mb-3 text-sm" style="text-align: left;">&nbsp;</h6>
                                                <span class="mb-2 text-xs" style="text-align: left;">Mobile No.: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("reported_by") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Name: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("user_name") %></span></span>
                                            </div>
                                            <div class="ms-auto text-end">
                                                <h6 class="mb-3 text-sm">Ref No.: <%# Eval("report_refno") %></h6>
                                                <asp:LinkButton ID="lbtn" runat="server" CommandName="ACTION" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-outline-success btn-sm mb-0" Style="text-transform: none;">Acknowledge</asp:LinkButton>
                                                <asp:LinkButton ID="lbtnviewmap" runat="server" CommandName="VIEWMAP" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-outline-danger btn-sm mb-0" Style="text-transform: none;">View On Map</asp:LinkButton>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvGrievance" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="w-100" OnRowCommand="gvGrievance_RowCommand" ShowHeader="false" DataKeyNames="g_status,g_refno">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <li class="list-group-item border-0 d-flex p-4 mb-2 bg-gray-100 border-radius-lg">

                                            <div class="d-flex flex-column">
                                                <h6 class="mb-3 text-sm" style="text-align: left; color:#074886"><%# Eval("category_name") %> - <%# Eval("sub_categoryname") %></h6>
                                                <span class="mb-2 text-xs" style="text-align: left;">Remark: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("g_remark") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Date Time: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("g_datetime") %></span></span>
                                            </div>
                                            <div class="d-flex flex-column ps-5">
                                                <h6 class="mb-3 text-sm" style="text-align: left;">&nbsp;</h6>
                                                <span class="mb-2 text-xs" style="text-align: left;">Mobile No.: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("g_byuser") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Name: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("user_name") %></span></span>
                                            </div>
                                            <div class="ms-auto text-end">
                                                <h6 class="mb-3 text-sm">Ref No. : <%# Eval("g_refno") %></h6>
                                                <asp:LinkButton ID="lbtn" runat="server" CommandName="ACTION" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-outline-danger btn-sm mb-0">Action</asp:LinkButton>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvBusList" runat="server" PageSize="10" GridLines="None" AutoGenerateColumns="false" ClientIDMode="Static"
                            CssClass="w-100" OnRowCommand="gvBusList_RowCommand" ShowHeader="true" OnRowDataBound="gvBusList_RowDataBound" DataKeyNames="busno,gpsyn_ , office,service">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="list-group-item border-0 d-flex p-4 mb-2 bg-gray-100 border-radius-lg p-0">

                                            <div class="d-flex flex-column">
                                                <%-- <h6 class="mb-3 text-sm" style="text-align: left;">Bus No - <%# Eval("busno") %></h6>--%>
                                                <span class="mb-2 text-xs" style="text-align: left;">Bus No: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("busno") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Office: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("office") %></span></span>
                                            </div>
                                            <div class="d-flex flex-column ps-xl-10 ps-lg-4 ps-md-2 ps-sm-1 ps-3">

                                                <span class="mb-2 text-xs" style="text-align: left;">Current Status: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("dutystatus_") %></span></span>
                                                <span class="text-xs" style="text-align: left;">Service Type: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("service") %></span></span>
                                            </div>
                                            <div class="ms-auto text-end">
                                                <%--<h6 class="mb-3 text-sm" style="">GPS Status - <%# Eval("gpsyn_") %></h6>--%>
                                                <asp:LinkButton ID="lbtnTrack" runat="server" CommandName="ACTION" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-outline-danger btn-sm mb-0 mt-0">Track</asp:LinkButton>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvEmpList" runat="server" PageSize="10" GridLines="None" AutoGenerateColumns="false" ClientIDMode="Static"
                            CssClass="w-100" OnRowCommand="gvEmpList_RowCommand" OnRowDataBound="gvEmpList_RowDataBound" ShowHeader="true" DataKeyNames="empcode_,name_, mobile_ ,gender ,reportingoffice_,postingoffice,designation">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="list-group-item border-0 d-flex p-4 mb-2 bg-gray-100 border-radius-lg p-0">

                                            <div class="d-flex flex-column">
                                                <%-- <h6 class="mb-3 text-sm" style="text-align: left;">Bus No - <%# Eval("busno") %></h6>--%>
                                                <span class="mb-1 text-xs" style="text-align: left;">Empcode: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("empcode_") %></span></span>
                                                <span class="mb-1 text-xs" style="text-align: left;">Employee Name: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("name_") %>(<%# Eval("gender") %>)</span></span>
                                                <span class="text-xs" style="text-align: left;">Mobile: <span class="text-dark font-weight-bold ms-sm-2"><%# Eval("mobile_") %></span></span>

                                            </div>
                                            <div class="d-flex flex-column ps-xl-10 ps-lg-4 ps-md-2 ps-sm-1 ps-3">
                                                <span class=" mb-1 text-xs" style="text-align: left;">Designation: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("designation") %></span></span>
                                                <span class="mb-1 text-xs" style="text-align: left;">Posting Office: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("postingoffice") %></span></span>

                                                <span class="mb-1 text-xs" style="text-align: left;">Reporting Office: <span class="text-dark ms-sm-2 font-weight-bold"><%# Eval("reportingoffice_") %></span></span>
                                            </div>
                                            <div class="ms-auto text-end">
                                                <%--<h6 class="mb-3 text-sm" style="">GPS Status - <%# Eval("gpsyn_") %></h6>--%>
                                                <asp:LinkButton ID="lbtnTrack" runat="server" CommandName="ACTION" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-outline-danger btn-sm mb-0 mt-0">Track</asp:LinkButton>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ul>
                    <asp:Panel ID="pnlNoRecord" runat="server" CssClass="py-8 text-center">
                        <i class="ni ni-collection ni-5x"></i>
                        <h2 class="">No record here</h2>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <cc1:ModalPopupExtender ID="mpAck" runat="server" CancelControlID="btnClosempAck" TargetControlID="btnOpenmpAck"
            PopupControlID="pnlmpAck" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpAck" Style="display: none" runat="server">
            <div class="modal-content modal-dialog modal-lg">
                <div class="card">
                    <div class="card-header py-3">
                        <span class="me-auto font-weight-bold" style="color: black;">Acknowledgement</span>
                    </div>
                    <hr class="horizontal dark m-0" />
                    <div class="card-body">
                        <p style="color: black;">
                            Ref No.
                            <asp:Label ID="lblmpAckRefNo" runat="server" CssClass="font-weight-bold"></asp:Label>
                            <asp:HiddenField ID="hfmpAckRefNo" runat="server" Value="0"></asp:HiddenField>
                        </p>
                        <asp:TextBox ID="tbmpAckremark" runat="server" CssClass="form-control border p-2" MaxLength="200" TextMode="MultiLine" placeholder="Enter remark here..." Style="resize: none;"></asp:TextBox>
                        <p class="text-end text-xs mb-0">Maximum 200 character</p>
                    </div>
                    <div class="card-footer text-end">
                        <asp:LinkButton ID="btnClosempAck" runat="server" CssClass="btn btn-warning mb-0">Cancel</asp:LinkButton>
                        <asp:LinkButton ID="lbtnmpAckAcknowledged" runat="server" CssClass="btn btn-success mb-0 ms-2">Acknowledged</asp:LinkButton>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div style="visibility: hidden;">
            <asp:Button ID="btnOpenmpAck" runat="server" />
        </div>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpGrievance" runat="server" PopupControlID="pnlmpGrievance" TargetControlID="btnOpenmpGrievance"
            CancelControlID="lbtnClosempGrievance" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpGrievance" runat="server" Style="position: fixed;">
            <div class="modal-content  modal-dialog mt-3" style="width: 90vw;">
                <div class="card">
                    <div class="card-header py-3">
                        <div class="row">
                            <div class="col">
                                <h3 class="m-0">Grievance</h3>
                            </div>
                            <div class="col-auto">
                                <asp:LinkButton ID="lbtnClosempGrievancee" runat="server" OnClick="lbtnClosempGrievancee_Click" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body p-2">
                        <%--     <embed src = "dashGrievance.aspx" style="height: 80vh; width: 100%"  />--%>
                        <asp:Literal ID="eDash" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpGrievance" runat="server" Text="" />
                <asp:Button ID="lbtnClosempGrievance" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpError" runat="server" CancelControlID="lbtnClosempError" TargetControlID="btnOpenmpError"
            PopupControlID="pnlmpError" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpError" Style="display: none" runat="server">
            <div class="modal-content modal-dialog modal-lg">
                <div class="card">
                    <div class="card-header py-3">
                        <span class="me-auto font-weight-bold" style="color: black;">
                            <asp:Label ID="lblmpErrorHeader" runat="server"></asp:Label></span>
                        <asp:LinkButton ID="lbtnClosempError" runat="server" CssClass="float-end text-danger"><i class="fa fa-times" style="font-size:22px;"></i></asp:LinkButton>
                    </div>
                    <hr class="horizontal dark m-0" />
                    <div class="card-body">
                        <asp:Label ID="lblmpErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div style="visibility: hidden;">
            <asp:Button ID="btnOpenmpError" runat="server" />
        </div>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpTrack" runat="server" PopupControlID="pnlTrack" TargetControlID="btnOpenmpTrack"
            CancelControlID="lbtnCloseTrack" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlTrack" runat="server" Style="position: fixed;">
            <div class="modal-content  modal-dialog mt-3" style="width: 70vw;">
                <div class="card">
                    <div class="card-header py-3">
                        <div class="row">
                            <div class="col">
                                <h3 class="m-0">Track Bus</h3>
                            </div>
                            <div class="col-auto">
                                <asp:LinkButton ID="lbtnCloseTrack" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body p-2">
                       
                        <asp:Literal ID="eTrack" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpTrack" runat="server" Text="" />
                <asp:Button ID="Button22" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>

