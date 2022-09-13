// Copyright 2022 Niantic, Inc. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Linq;

using Niantic.ARDK.AR.WayspotAnchors;

using UnityEngine;

namespace WayspotAnchors
{
    public static class WayspotAnchorDataUtility
    {
        private const string DataKey = "wayspot_anchor_payloads";

        public static void SaveLocalPayloads(WayspotAnchorPayload[] wayspotAnchorPayloads)
        {
            var wayspotAnchorsData = new WayspotAnchorsData();
            wayspotAnchorsData.Payloads = wayspotAnchorPayloads.Select(a => a.Serialize()).ToArray();
            string wayspotAnchorsJson = JsonUtility.ToJson(wayspotAnchorsData);
            PlayerPrefs.SetString(DataKey, wayspotAnchorsJson);
        }

        public static WayspotAnchorPayload[] LoadLocalPayloads()
        {
            //if (PlayerPrefs.HasKey(DataKey))
            //{
            var payloads = new List<WayspotAnchorPayload>();
            // var json = PlayerPrefs.GetString(DataKey);
            // var wayspotAnchorsData = JsonUtility.FromJson<WayspotAnchorsData>(json);
            var payloads_serialized = new string[0];

            if (Stateholder.pois[Stateholder.curr_poi].following != null)
            {
                if (Stateholder.pois[Stateholder.curr_poi].following[Stateholder.curr_schnitzel_counter].anchors != null)
                {
                    payloads_serialized = Stateholder.pois[Stateholder.curr_poi].following[Stateholder.curr_schnitzel_counter].anchors;
                }
                else
                {
                    return Array.Empty<WayspotAnchorPayload>();
                }
            }
            else
            {
                if (Stateholder.pois[Stateholder.curr_poi].anchors != null)
                {
                    payloads_serialized = Stateholder.pois[Stateholder.curr_poi].anchors;
                }
                else
                {
                    return Array.Empty<WayspotAnchorPayload>();
                }
            }
            foreach (var wayspotAnchorPayload in payloads_serialized)
            {
                var payload = WayspotAnchorPayload.Deserialize(wayspotAnchorPayload);
                payloads.Add(payload);
            }

            return payloads.ToArray();
            //}
            //else
            //{
            //  return Array.Empty<WayspotAnchorPayload>();
            //}
        }

        public static void ClearLocalPayloads()
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                PlayerPrefs.DeleteKey(DataKey);
            }
        }

        [Serializable]
        private class WayspotAnchorsData
        {
            /// The payloads to save via JsonUtility
            public string[] Payloads = Array.Empty<string>();
        }
    }
}
