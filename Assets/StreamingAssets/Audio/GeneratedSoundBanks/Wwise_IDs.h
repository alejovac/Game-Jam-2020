/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID GAME_START = 733168346U;
        static const AkUniqueID PL_BUY = 3055392428U;
        static const AkUniqueID PL_PLANT_ADD = 1022277923U;
        static const AkUniqueID PL_PLANT_REMOVE = 1646982530U;
        static const AkUniqueID PL_SELL = 3824442600U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace FX
        {
            static const AkUniqueID GROUP = 1802970371U;

            namespace STATE
            {
                static const AkUniqueID MUTE_SFX = 1305760402U;
                static const AkUniqueID UNMUTE_SFX = 141872311U;
            } // namespace STATE
        } // namespace FX

        namespace GAME
        {
            static const AkUniqueID GROUP = 702482391U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
            } // namespace STATE
        } // namespace GAME

        namespace MX
        {
            static const AkUniqueID GROUP = 1685527054U;

            namespace STATE
            {
                static const AkUniqueID MUTE_MX = 1814839050U;
                static const AkUniqueID UNMUTE_MX = 3963825173U;
            } // namespace STATE
        } // namespace MX

        namespace PLANTS_STATE
        {
            static const AkUniqueID GROUP = 2735915761U;

            namespace STATE
            {
                static const AkUniqueID HIGH = 3550808449U;
                static const AkUniqueID LOW = 545371365U;
                static const AkUniqueID MEDIUM = 2849147824U;
            } // namespace STATE
        } // namespace PLANTS_STATE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID HUMIDITY = 3577765598U;
        static const AkUniqueID VOLUME_FX = 135182116U;
        static const AkUniqueID VOLUME_MX = 319736021U;
        static const AkUniqueID WILDLIFE = 1118910643U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID GGJ20 = 2182974741U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MX = 1685527054U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
