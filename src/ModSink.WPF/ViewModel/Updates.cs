﻿using ReactiveUI;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModSink.WPF.ViewModel
{
    public class Updates : ReactiveObject
    {
        public Updates(UpdateManager mgr)
        {
            this.mgr = mgr;

            var chk = ReactiveCommand.CreateFromTask(async () => await this.mgr.CheckForUpdate());
            chk.ToProperty(this, x => x.UpdateInfo);
            this.CheckUpdate = chk;

            Download = ReactiveCommand.CreateFromTask<IEnumerable<ReleaseEntry>>(async (releases) => await mgr.DownloadReleases(releases));

            ApplyUpdate = ReactiveCommand.CreateFromTask<UpdateInfo>(async (updateInfo) => await mgr.ApplyReleases(updateInfo));
        }

        public ReactiveCommand ApplyUpdate { get; protected set; }
        public ReactiveCommand Download { get; protected set; }
        public ReactiveCommand CheckUpdate { get; protected set; }

        public UpdateInfo UpdateInfo { get; set; }
        private Squirrel.UpdateManager mgr { get; }
    }
}